package rooms;

import commands.server.*;
import core.Context;
import dto.EndTurnData;
import dto.TurnData;
import players.Player;

import java.util.*;


/**
 * @author trollingchar
 */
public class Battle extends Room {
    // field 'players' inherited - they are observers here
    private Queue<Player> activePlayers;
    private Player currentPlayer;
    private Map<Player, EndTurnData> etdMap = new HashMap<>();

    /* rules:
     *  server broadcasts players order and rng seed
     *  first player makes his turn
     *  when each player sees no need for synchronizing, they send sync-message with checksum
     *  when checksums differ, end battle and log error
     *  when all checksums received, start new turn
     */

    // receive commands: turnData, syncData, quitGame


    @Override
    protected void onPlayerRemoved (Player player) {
        broadcast(new LeftGameCmd(player.getId()));
    }


    public void start () {
        List<Player> playerList = new ArrayList<>(players);
        Collections.shuffle(playerList, Context.random);
        activePlayers = new LinkedList<>(playerList);

        // init all
        broadcast(new NewGameCmd(Context.random.nextInt(), activePlayers));
    }


    public void onTurnData (Player player, TurnData turnData) {
        if (player != currentPlayer) {
            player.logCheating("is sending turn data during turn of other player");
            return;
        }
        broadcast(new UpdateWorldCmd(turnData), p -> p != player);
    }


    public void onEndTurn (Player player, EndTurnData endTurnData) {
        if (!endTurnData.alive) {
            // remove player from active players and notify players
        }
        if (etdMap.put(player, endTurnData) != null) {
            player.logCheating("is sending end turn data more than once");
        }
        // check if all players sent end turn data
        newTurn();
    }


    private void newTurn () {
        etdMap.clear();
        if (checkVictoryConditions()) return;
        // select next active player and notify players about that
        broadcast(new NewTurnCmd(currentPlayer.getId()));
    }


    protected boolean checkVictoryConditions () {
        switch (activePlayers.size()) {
            case 0:
                broadcast(GameEndedCmd.draw());
                break;
            case 1:
                broadcast(GameEndedCmd.victory(activePlayers.peek().getId()));
                break;
            default:
                return false;
        }
        return true;
    }


    private void onDesync () {
        System.out.println("ERROR - BATTLE DESYNCRONIZED");
        broadcast(GameEndedCmd.desync());
    }
}
