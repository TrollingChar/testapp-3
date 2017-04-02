import java.util.LinkedList;
import java.util.Queue;

/**
 * Created by Дима on 02.04.2017.
 */
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
            room.add(players.remove());
        }
        room.startGame();
    }

    public void remove(Player player) {
        players.remove(this);
    }
}
