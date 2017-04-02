import org.eclipse.jetty.websocket.api.Session;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by Дима on 02.04.2017.
 */
public class Lookup {
    static public Map<Session, Player> players = new HashMap<>();
    static public Map<Integer, Player> playersId = new HashMap<>();

    public static Player getPlayer(Session session) {
        return players.get(session);
    }

    public static Player getPlayer(int id) {
        return playersId.get(id);
    }

    public static Player addPlayer(Session session, int id) throws Exception {
        if(players.containsKey(session)) throw new Exception();
        if(playersId.containsKey(id)) throw new Exception();
        Player player = new Player(session, id);
        players.put(session, player);
        playersId.put(id, player);
        return player;
    }

    public static void removePlayer(Session session, int id) {
        players.remove(session);
        playersId.remove(id);
    }
}
