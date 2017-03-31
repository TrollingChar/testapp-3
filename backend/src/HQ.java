import org.eclipse.jetty.websocket.api.Session;

/**
 * Created by Дима on 31.03.2017.
 */
public class HQ {
    public static void authorize(Session sess, int id) {
        System.out.println("player " + id + " connected");
    }

    public static void onStartGame(Session sess) {
        System.out.println("starting game (no)");
    }

    public static void onCancel(Session sess) {
        System.out.println("cancelling game (no)");
    }

    public static void onCheat(Session sess) {
        System.out.println("cheating (yes)");
    }

    public static void onQuitGame(Session sess) {
        System.out.println("leaving game (no)");
    }

    public static void onSync(Session sess) {
        System.out.println("synchronizing game (no)");
    }

    public static void onTurnData(Session sess) {
        System.out.println("updating game (no)");
    }

    public static void disconnect(Session sess) {
        System.out.println("disconnecting client");
        sess.close();
    }
}
