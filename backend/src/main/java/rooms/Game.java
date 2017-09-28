package rooms;

import core.Root;
import dto.data.EndTurnData;
import dto.data.TurnData;
import dto.server.*;
import players.Player;

import java.util.*;


public class Game extends Room {

    private boolean singlePlayer;
    private Player current;
    private Queue<Player> queue;
    private Map<Player, EndTurnData> etdMap;


    public Game (List<Player> players) {
        for (Player player : players) player.joinRoom(this);
        start();
    }


    private void start () {
        if (players.size() == 1) {
            singlePlayer = true;
        }
        Collections.shuffle(players);
        queue = new LinkedList<>(players);
        etdMap = new HashMap<>();
        broadcast(new NewGameCmd(Root.random.nextInt(), players));
        // newTurn(); - DO NOT DO THIS - wait until they synchronize!
    }


    private void newTurn () {
        etdMap.clear();
        int playersLeft = queue.size();

        if (playersLeft == 0) {
            broadcast(GameEndedCmd.draw());
            clear();
        }
        else if (playersLeft == 1 && !singlePlayer) {
            broadcast(GameEndedCmd.victory(queue.poll()));
            clear();
        }
        else {
            current = queue.poll();
            queue.add(current);
            broadcast(new NewTurnCmd(current.id));
        }
    }


    @Override
    protected void onRemovePlayer (Player player) {
        broadcast(new LeftGameCmd(player.id));
        queue.remove(player);
        if (player == current) {
            newTurn();
        }
    }


    public void processTurnData (Player player, TurnData data) {
        if (player != current) return; // he is cheating!
        broadcast(new TurnDataSCmd(data), p -> p != player);
    }


    public void processEndTurn (Player player, EndTurnData data) {
        etdMap.put(player, data);
        if (!data.alive) {
            queue.remove(player);
        }
        else {
            etdMap.put(player, data);
        }
        if (etdMap.size() == queue.size()) newTurn();
    }
}
