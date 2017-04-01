import org.eclipse.jetty.websocket.api.Session;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

/**
 * Created by Дима on 31.03.2017.
 */
public class HQ {
    static Map<Session, Player> players = new HashMap<>();
    static Map<Integer, Player> playersId = new HashMap<>();

    static Player getPlayer(int id) {
        return playersId.get(id);
    }
    static Player getPlayer(Session session) {
        return players.get(session);
    }

    public static void authorize(Session session, int id) throws IOException {
        System.out.print("player " + id + " connecting... ");
        if(playersId.containsKey(id)) {
            System.out.println("error: duplicate id");
            session.close();
            return;
        }
        System.out.println("ok");
        Player player = new Player(session, id);
        players.put(session, player);
        playersId.put(id, player);
        player.sendAccountData();
    }

    public static void onStartGame(Session session) {
        System.out.println("player " + getPlayer(session).id + " ready to start game");
        // move him to lobby
    }

    public static void onCancel(Session session) {
        System.out.println("player " + getPlayer(session).id + " cancelling game...");
        // remove him from lobby if he is not playing
    }

    public static void onCheat(Session session) {
        System.out.println("player " + getPlayer(session).id + " cheating!");
    }

    public static void onQuitGame(Session session) {
        System.out.println("player " + getPlayer(session).id + " leaving game");
    }

    public static void onSync(Session session) {
        System.out.println("player " + getPlayer(session).id + " synchronizing game");
    }

    public static void onTurnData(Session session) {

    }

    public static void disconnect(Session session) {
        System.out.print("disconnecting player");
        Player player = players.remove(session);
        if(player != null) {
            System.out.print(" " + player.id);
            playersId.remove(player.id);
        }
        System.out.println();
        session.close();
    }
}
