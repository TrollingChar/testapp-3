package players;

import io.netty.channel.ChannelHandlerContext;
import io.netty.util.AttributeKey;
import util.Dispatcher;


public class Players {

    public static final Dispatcher<Player> onPlayerConnected = new Dispatcher<>();
    public static final Dispatcher<Player> onClientDisconnected = new Dispatcher<>();
    private static final AttributeKey<Player> KEY = AttributeKey.newInstance("player");


    public static Player register (ChannelHandlerContext chan, int id) {
        if (chan.hasAttr(KEY)) {
            System.err.println("player already registered");
            return null;
        }
        Player player = new Player(chan, id);
        chan.attr(KEY).set(player);
        onPlayerConnected.dispatch(player);
        return player;
    }


    public static Player get (ChannelHandlerContext chan) {
        return chan.attr(KEY).get();
    }


    public static void remove (ChannelHandlerContext chan) {
        if (!chan.hasAttr(KEY)) {
            System.err.println("no such player");
            return;
        }
        Player player = chan.attr(KEY).getAndSet(null);
        onClientDisconnected.dispatch(player);
    }
}
