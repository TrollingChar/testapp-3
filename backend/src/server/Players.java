package server;

import org.eclipse.jetty.websocket.api.Session;

import java.util.HashMap;
import java.util.Map;

/**
 * Определение игроков по сессии или номеру.
 */
public class Players {
    static Map<Session, Player> sessionPlayerMap;
    static Map<Integer, Player> integerPlayerMap;

    static void init() {
        System.out.println("Players.init");
        sessionPlayerMap = new HashMap<>();
        integerPlayerMap = new HashMap<>();
    }

    static synchronized Player add(Session session, int id) throws Exception {
        System.out.println("Players.add");
        if(sessionPlayerMap.containsKey(session)) throw new Exception("session already exists");
        if(integerPlayerMap.containsKey(id)) throw new Exception("id already exists");
        Player player = new Player(session, id);
        sessionPlayerMap.put(session, player);
        integerPlayerMap.put(id, player);
        return player;
    }

    static synchronized Player get(Session session) {
        System.out.println("Players.get(session)");
        return sessionPlayerMap.get(session);
    }

    static synchronized Player get(int id) {
        System.out.println("Players.get(id)");
        return integerPlayerMap.get(id);
    }

    static synchronized void remove(Player player) {
        System.out.println("Players.remove");
        sessionPlayerMap.remove(player.session);
        integerPlayerMap.remove(player.id);
    }
}
