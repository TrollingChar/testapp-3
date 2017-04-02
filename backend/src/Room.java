import java.io.IOException;
import java.nio.ByteBuffer;
import java.util.Collection;
import java.util.LinkedList;
import java.util.Queue;
import java.util.Random;

/**
 * Created by Дима on 02.04.2017.
 */
public class Room {
    static Random rnd = new Random();
    Queue<Player> players = new LinkedList<>();

    public void add(Player player) {
        players.add(player);
    }

    public void startGame() {
        syncPlayers();
        for (Player player : players) player.room = this;

        // start game
        if(players.size() < 2) close();
    }

    private void close() {
        while (players.size() > 0) try {
            Player winner = players.remove();
            winner.room = null;
            winner.sendWin();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void syncPlayers() {
        byte[] bytes = new byte[4];
        rnd.nextBytes(bytes); // random seed for game

        boolean flag = false;
        Queue<Player> online = new LinkedList<>(),
                     offline = new LinkedList<>();
        for (Player player : players) {
            try {
                player.sendStartGame(bytes, players);
                online.add(player);
            } catch (Throwable e) {
                offline.add(player);
                flag = true;
            }
        }
        while (flag) {
            players = online;
            Queue<Player> left = offline;
            online = new LinkedList<>();
            offline = new LinkedList<>();
        }
    } // void syncPlayers()

    public void remove(Player player) {
        players.remove(player);
        player.room = null;
        for (Player other : players) {
            try {
                other.sendPlayerLeft(player);
            } catch (Throwable e) {
                System.out.println("i hate u, jetty");
            }
        }
        if(players.size() < 2) close();
    }
}
