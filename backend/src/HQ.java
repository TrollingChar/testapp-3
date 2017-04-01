import org.eclipse.jetty.websocket.api.Session;

import java.io.IOException;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.Map;
import java.util.Queue;

public class HQ {
    static Map<Session, Player> players = new HashMap<>();
    static Map<Integer, Player> playersId = new HashMap<>();

    static Queue<Player>[] hubs = new Queue[256];

    public static void init() {
        for(byte i = 1; i != 0; ++i) hubs[i] = new LinkedList<>();
        // hubs[0] == null
    }

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

    public static void movePlayerToHub(Session session, byte hub) throws IOException {
        // hub 0 means that player is cancelling game
        Player player = getPlayer(session);
        if(player.room != null) return;
        if(player.hub == 0) {
            if(hub != 0) {
                player.hub = hub;
                hubs[hub].add(player);
                checkHub(hub);
            }
        } else {
            hubs[hub].remove(player);
            player.hub = hub;
            if(hub == 0) {
                player.sendConfirmCancel();
            } else {
                hubs[hub].add(player);
                checkHub(hub);
            }
        }
        System.out.println("player " + getPlayer(session).id + " moved to hub " + hub);
    }

    private static void checkHub(byte hub) throws IOException {
        if(hubs[hub].size() >= hub) {
            Room room = new Room();
            for (int i = 0; i < hub; ++i) room.addPlayer(hubs[hub].remove());
            room.startGame();
        }
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
