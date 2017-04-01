import java.io.IOException;
import java.util.LinkedList;

public class Room {
    LinkedList<Player> players;

    public Room() {
        players = new LinkedList<>();
    }

    public void addPlayer(Player player) {
        player.room = this;
        players.add(player);
    }

    public void startGame() throws IOException {
        for (Player player : players) {
            player.sendStartGame(players);
        }
    }
}
