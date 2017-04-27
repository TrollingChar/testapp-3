package server;

import java.io.IOException;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Set;

/**
 * Место, где собираются игроки перед игрой 
 * Игра начинается, как только набралось
 * нужное количество игроков.
 */
public class Hub {
    int playersNeeded;
    Set<Player> players;

    public Hub(int playersNeeded) {
        System.out.println("Hub.Hub");
        this.playersNeeded = playersNeeded;
        players = new HashSet<>();
    }

    public synchronized void add(Player player) {
        System.out.println("Hub.add");
        player.hub = this;
        players.add(player);
        sendPlayersCount();
        if (players.size() == playersNeeded) startGame();
    }

    public synchronized void removeSilently(Player player) {
        System.out.println("Hub.removeSilently");
        players.remove(player);
        player.hub = null;
        sendPlayersCount();
    }

    public synchronized void remove(Player player) throws IOException {
        System.out.println("Hub.remove");
        players.remove(player);
        player.hub = null;
        sendPlayersCount();
        player.sendHubStatus(0, 0);
    }

    void sendPlayersCount() {
        System.out.println("Hub.sendPlayersCount");
        Player player;
        int playersCount = players.size();
        for (Iterator<Player> it = players.iterator(); it.hasNext(); ) {
            player = it.next();
            try {
                player.sendHubStatus(playersNeeded, playersCount);
            } catch (IOException e) {
                player.hub = null;
                it.remove();
                it = players.iterator();
                player.disconnect();
            }
        }
    }

    void startGame() {
        System.out.println("Hub.startGame");
        Room room = new Room(players);
        for (Player player : players) player.hub = null;
        players.clear();
        room.startGame();
    }
}
