package rooms;

import commands.server.LobbyUpdatedCmd;
import commands.server.RemovedFromLobbyCmd;
import players.Player;


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
        for (Player player : players) {
            player.joinRoom(battle);
            player.quitRoom(this);
        }
        battle.start();
    }
}
