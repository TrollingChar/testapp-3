import java.util.LinkedList;
import java.util.Queue;

public class Hub {
    int roomCapacity;
    Queue<Player> players;

    public Hub(int roomCapacity) {
        this.roomCapacity = roomCapacity;
        players = new LinkedList<>();
    }

    public void add(Player player) {
        players.add(player);
        check();
    }

    private void check() {
        if(players.size() < roomCapacity) return;
        Room room = new Room();
        for (int i = 0; i < roomCapacity; ++i) {
            Player player = players.remove();
            player.hub = null;
            room.add(player);
        }
        room.startGame();
    }

    public void remove(Player player) {
        players.remove(this);
    }
}
