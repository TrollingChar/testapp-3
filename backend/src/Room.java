import java.nio.ByteBuffer;
import java.util.LinkedList;
import java.util.Queue;
import java.util.Random;

/**
 * Created by Дима on 02.04.2017.
 */
public class Room {
    static Random rnd = new Random();
    Queue<Player> players;

    public void add(Player player) {
        players.add(player);
    }

    public void startGame() {
        syncPlayers();

        // start game
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
            for (Player player : players) {
                try {
                    player.sendPlayersLeft(left);
                    online.add(player);
                } catch (Throwable e) {
                    offline.add(player);
                }
            }
        }
    } // void syncPlayers()
}
