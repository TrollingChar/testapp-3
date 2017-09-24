package rooms;

import dto.cmd.server.ServerCommand;
import players.Player;

import java.util.ArrayList;
import java.util.Collection;
import java.util.LinkedList;
import java.util.function.Predicate;


public class Room {

    private Collection<Player> players;


    public synchronized void addPlayer (Player player) {
        players.add(player);
    }


    public synchronized void removePlayer (Player player) {
        players.remove(player);
    }


    public synchronized void clear () {
        players.clear();
    }


    public synchronized void broadcast (ServerCommand cmd) {
        for (Player player : new ArrayList<>(players)) player.send(cmd);
    }


    public synchronized void broadcast (ServerCommand cmd, Predicate<Player> predicate) {
        LinkedList<Player> receivers = new LinkedList<>(players);
        players.removeIf(predicate.negate());
        for (Player player : receivers) player.send(cmd);
    }
}
