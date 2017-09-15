package players;

import org.eclipse.jetty.websocket.api.Session;

import java.util.HashMap;
import java.util.Map;


public class Players {

    private static Map<Integer, Player> players = new HashMap<>();


    private Players () {
        // no instance
    }


    public static Player login (Session session, int id) {
        Player player = new Player(id, session);
        if (players.put(id, player) != null) {
            // todo: disconnect him and register new player with that id
            return null;
        }
        return player;
    }


    public static void logout (int id) {
        players.remove(id);
    }
}
