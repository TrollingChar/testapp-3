package server.players;

import org.eclipse.jetty.websocket.api.Session;

import java.util.HashMap;
import java.util.Map;

/**
 * Определение игроков по сессии или номеру.
 */
public class Players {
    private static Map<Session, PlayerImpl> sessionPlayerMap;
    private static Map<Integer, PlayerImpl> integerPlayerMap;

    public static void init() {
        System.out.println("Players.init");
        sessionPlayerMap = new HashMap<>();
        integerPlayerMap = new HashMap<>();
    }

    public static synchronized PlayerImpl add(Session session, int id) throws Exception {
        System.out.println("Players.add");
        if(sessionPlayerMap.containsKey(session)) throw new Exception("session already exists");
        if(integerPlayerMap.containsKey(id)) throw new Exception("id already exists");
        PlayerImpl player = new PlayerImpl(session, id);
        sessionPlayerMap.put(session, player);
        integerPlayerMap.put(id, player);
        return player;
    }

    public static synchronized PlayerImpl get(Session session) {
        System.out.println("Players.get(session)");
        return sessionPlayerMap.get(session);
    }

    static synchronized PlayerImpl get(int id) {
        System.out.println("Players.get(id)");
        return integerPlayerMap.get(id);
    }

    static synchronized void remove(PlayerImpl player) {
        System.out.println("Players.remove");
        sessionPlayerMap.remove(player.session);
        integerPlayerMap.remove(player.id);
    }
}
