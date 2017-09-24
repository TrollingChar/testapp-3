package rooms;

import dto.server.LobbyStatusCmd;
import players.Player;


// accumulates N players then creates a game for them
public class Lobby extends Room {

    private int size;


    public Lobby (int size) {
        this.size = size;
    }


    @Override
    protected void onPlayerAdded (Player player) {
        notifyPlayers();
        if (players.size() >= size) {
            new Game(players);
            clear();
        }
    }


    @Override
    protected void onPlayerRemoved (Player player) {
        notifyPlayers();
    }


    private void notifyPlayers () {
        new LobbyStatusCmd(size, players.size());
    }
}
