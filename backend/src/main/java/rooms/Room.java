package rooms;

import dto.server.ServerCommand;
import players.Player;

import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;
import java.util.function.Predicate;


public abstract class Room {

    protected List<Player> players = new ArrayList<>();


    public final void addPlayer (Player player) {
        // called from Player
        onAddPlayer(player);
        players.add(player);
        onPlayerAdded(player);
    }


    public final void removePlayer (Player player) {
        // called from Player
        onRemovePlayer(player);
        players.remove(player);
        onPlayerRemoved(player);
    }


    protected final void kickPlayer (Player player) {
        players.remove(player);
        player.onKick(this);
        OnKickPlayer(player);
    }


    public final void clear () {
        for (Player player : players) player.onKick(this);
        players.clear();
    }


    public final void broadcast (ServerCommand cmd) {
        for (Player player : new ArrayList<>(players)) player.send(cmd);
    }


    public final void broadcast (ServerCommand cmd, Predicate<Player> predicate) {
        LinkedList<Player> receivers = new LinkedList<>(players);
        players.removeIf(predicate.negate());
        for (Player player : receivers) player.send(cmd);
    }


    protected void onAddPlayer (Player player) {
    }


    protected void onPlayerAdded (Player player) {
    }


    protected void onRemovePlayer (Player player) {
    }


    protected void onPlayerRemoved (Player player) {
    }


    protected void OnKickPlayer (Player player) {
    }
}
