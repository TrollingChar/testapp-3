package server.rooms;

import server.event.server.ServerEvent;
import server.players.Player;

import java.util.List;
import java.util.function.Predicate;

public class Room {
    List<Player> players;

    public void addPlayer(Player player) {
        players.add(player);
    }

    public void removePlayer(Player player) {
        players.remove(player);
    }

    public void broadcast(ServerEvent cmd) {
        for (Player p : players) p.send(cmd);
    }

    public void broadcast(ServerEvent cmd, Predicate<Player> predicate) {
        for (Player p : players) {
            if (predicate.test(p)) p.send(cmd);
        }
    }
}
