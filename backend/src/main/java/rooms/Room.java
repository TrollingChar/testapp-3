package rooms;

import dto.cmd.server.ServerCommand;
import players.Player;

import java.util.ArrayList;
import java.util.Collection;
import java.util.function.Predicate;


public class Room {

    private Collection<Player> players;


    public void addPlayer (Player player) {
        players.add(player);
    }


    public void removePlayer (Player player) {
        players.remove(player);
    }


    public void clear () {
        players.clear();
    }


    public void broadcast (ServerCommand cmd) {
        for (Player player : new ArrayList<>(players)) player.send(cmd);
    }


    public void broadcast (ServerCommand cmd, Predicate<Player> predicate) {
        for (Player player : new ArrayList<>(players)) player.send(cmd);
    }
}
