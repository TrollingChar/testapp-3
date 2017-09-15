package rooms;

import commands.server.ServerCommand;
import players.Player;

import java.util.Collection;
import java.util.HashSet;
import java.util.function.Predicate;


public abstract class Room {
    protected Collection<Player> players = new HashSet<>();


    // todo: this interface can be used incorrect. Refactor it.
    public void addPlayer (Player player) {
        // todo: queue players if broadcast in progress
        players.add(player);
        onPlayerAdded(player);
    }


    public void removePlayer (Player player) {
        // todo: queue players
        players.remove(player);
        onPlayerRemoved(player);
    }


    protected void onPlayerAdded (Player player) {
    }


    protected void onPlayerRemoved (Player player) {
    }


    public void broadcast (ServerCommand cmd) {
        for (Player player : players) player.send(cmd);
    }


    public void broadcast (ServerCommand cmd, Predicate<Player> filter) {
        for (Player player : players) {
            if (filter.test(player)) player.send(cmd);
        }
    }
}
