package server.players;

import org.eclipse.jetty.websocket.api.Session;
import server.event.Dispatcher;
import server.event.client.AuthorizeEvent;
import server.event.server.AuthorizedEvent;

import java.util.HashMap;
import java.util.Map;

/**
 * Определение игроков по сессии или номеру.
 */
public class Players {
    private static Map<Session, Player> sessionPlayerMap;
    private static Map<Integer, Player> integerPlayerMap;

    static {
        sessionPlayerMap = new HashMap<>();
        integerPlayerMap = new HashMap<>();
        Dispatcher.addListener(Players::onAuthorizeEvent);
    }

    private static void onAuthorizeEvent(AuthorizeEvent event) {
        Player player = add(event.session, event.playerId);
        player.send(new AuthorizedEvent());
    }

    public static synchronized Player add(Session session, int id) throws Exception {
        if(sessionPlayerMap.containsKey(session)) throw new Exception("session already exists");
        if(integerPlayerMap.containsKey(id))      throw new Exception("id already exists");
        Player player = new PlayerImpl(session, id);
        sessionPlayerMap.put(session, player);
        integerPlayerMap.put(id, player);
        return player;
    }

    public static synchronized Player get(Session session) {
        return sessionPlayerMap.get(session);
    }

    static synchronized Player get(int id) {
        return integerPlayerMap.get(id);
    }

    static synchronized void remove(Player player) {
        sessionPlayerMap.remove(player.session);
        integerPlayerMap.remove(player.id);
    }
}
