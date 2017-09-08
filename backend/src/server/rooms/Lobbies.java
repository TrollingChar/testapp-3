package server.rooms;

import server.event.Dispatcher;
import server.event.client.JoinLobbyEvent;
import server.players.Player;

public class Lobbies {
    static Lobby[] lobbies = new Lobby[256];

    static {
        Dispatcher.addListener(Lobbies::onJoinHubEvent);
    }

    private static void onJoinHubEvent(JoinLobbyEvent event) {
        Player player = event.getPlayer();
        player.exitLobby();
        lobbies[event.lobbyId].addPlayer(player);
    }
}
