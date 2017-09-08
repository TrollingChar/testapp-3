package server.rooms;

import server.event.server.UpdateLobbyStatusEvent;
import server.players.Player;

public class Lobby extends Room {

    @Override
    public void addPlayer(Player player) {
        super.addPlayer(player);
        broadcast(new UpdateLobbyStatusEvent(players.size()));
        // todo: if enough players then create battle room and start game
        players.clear();
    }

    @Override
    public void removePlayer(Player player) {
        super.removePlayer(player);
        broadcast(new UpdateLobbyStatusEvent(players.size()));
    }
}
