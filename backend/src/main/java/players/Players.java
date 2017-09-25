package players;

import io.netty.channel.ChannelHandlerContext;
import io.netty.util.AttributeKey;
import util.Dispatcher;

import java.util.HashMap;
import java.util.Map;


public class Players {

    public static final Dispatcher<Player> onPlayerConnected = new Dispatcher<>();
    public static final Dispatcher<Player> onClientDisconnected = new Dispatcher<>();

    private static Map<Integer, Player> players = new HashMap<>();


    public static Player register (ChannelHandlerContext ctx, int id) {

        Player player = new Player(ctx, id);
        if (players.put(id, player) != null) {
            System.err.printf("id %d already registered%n", id);
        }
        return player;
    }


    public static Player get (int id) {
        return players.get(id);
    }


    public static void remove (int id) {
        if (players.remove(id) == null) {
            System.err.println("no player with id " + id);
        }
    }
}
