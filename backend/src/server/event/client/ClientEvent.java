package server.event.client;

import server.players.Player;

public abstract class ClientEvent {
    public static Player player;

    public Player getPlayer() {
        return player;
    }
}
