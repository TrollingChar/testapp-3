import java.io.IOException;
import java.nio.ByteBuffer;
import java.util.*;

public class Room {
    static Random rnd = new Random();
    Queue<Player> players = new LinkedList<>();

    public void sendStartGame(byte[] bytes) {
        // send message to all players
        Iterator<Player> it = players.iterator();
        LinkedList<Player> left = new LinkedList<>();
        while (it.hasNext()) {
            Player player = it.next();
            try {
                player.sendStartGame(bytes, players);
            } catch (Throwable e) {
                it.remove();
                removeAfterError(player);
                left.add(player);
            }
        }
        if (left.size() > 0) sendPlayersLeft(left);
    }

    public void sendPlayerLeft(Player player) {
        // that player has to be removed already
        Iterator<Player> it = players.iterator();
        LinkedList<Player> left = new LinkedList<>();
        while (it.hasNext()) {
            Player other = it.next();
            try {
                other.sendPlayerLeft(player);
            } catch (Throwable e) {
                it.remove();
                removeAfterError(other);
                left.add(other);
            }
        }
        if (left.size() > 0) sendPlayersLeft(left);
    }

    public void sendPlayersLeft(Collection<Player> players) {
        // players have to be removed already
        Iterator<Player> it = this.players.iterator();
        LinkedList<Player> left = new LinkedList<>();
        while (it.hasNext()) {
            Player player = it.next();
            try {
                player.sendPlayersLeft(players);
            } catch (Throwable e) {
                it.remove();
                removeAfterError(player);
                left.add(player);
            }
        }
        if (left.size() > 0) sendPlayersLeft(left);
    }

    private void sendTurnData(Player player, ByteBuffer data) {
        Iterator<Player> it = players.iterator();
        LinkedList<Player> left = new LinkedList<>();
        while (it.hasNext()) {
            Player other = it.next();
            if(player == other) continue;
            try {
                other.sendTurnData(data);
            } catch (Throwable e) {
                it.remove();
                removeAfterError(player);
                left.add(other);
            }
        }
        if (left.size() > 0) sendPlayersLeft(left);
    }

    public void add(Player player) {
        players.add(player);
        player.room = this;
    }

    public void startGame() {
        // shuffle players
        byte[] bytes = new byte[4];
        rnd.nextBytes(bytes); // random seed for game
        sendStartGame(bytes);
        if(players.size() < 2) endGame();
    }

    private void endGame() {
        Iterator<Player> it = players.iterator();
        while (it.hasNext()) {
            Player player = it.next();
            it.remove();
            try {
                player.sendWin();
                player.room = null;
            } catch (Throwable e) {
                removeAfterError(player);
            }
        }
    }

    private void removeAfterError(Player player) {
        // should be already removed by Iterator.remove
        player.room = null;
        HQ.disconnect(player); // do not send message to room bcz it's null
    }

    public void remove(Player player) {
        players.remove(player);
        player.room = null;
        sendPlayerLeft(player);
        if(players.size() < 2) endGame();
    }

    public void doTurn(Player player, ByteBuffer data) {
        sendTurnData(player, data);
    }

    public void endTurn(Player player, int frames, int hash) {

    }
}
