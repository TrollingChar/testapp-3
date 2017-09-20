package rooms;

import commands.server.LobbyUpdatedCmd;
import commands.server.RemovedFromLobbyCmd;
import players.Player;

import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;


public class Lobby extends Room {
    private int size;


    public Lobby (int size) {
        this.size = size;
    }


    @Override
    protected void onPlayerAdded (Player player) {
        broadcast(new LobbyUpdatedCmd(size, players.size()));
        if (size == players.size()) startGame();
    }


    @Override
    protected void onPlayerRemoved (Player player) {
        broadcast(new LobbyUpdatedCmd(size, players.size()));
        player.send(new RemovedFromLobbyCmd());
    }


    private void startGame () {
        Battle battle = new Battle();
        for (Player player : new HashSet<>(players)) {
            player.joinRoom(battle);
            player.quitRoom(this);
        }
        battle.start();
    }
}
