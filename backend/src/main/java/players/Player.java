package players;

import dto.cmd.server.ServerCommand;
import io.netty.channel.ChannelHandlerContext;


public class Player {

    public final ChannelHandlerContext ctx;
    private final int id;
    private boolean online;


    public Player (ChannelHandlerContext ctx, int id) {
        this.ctx = ctx;
        this.id = id;
        online = true;
    }


    public synchronized void send (ServerCommand cmd) {
        if (online) ctx.channel().writeAndFlush(cmd);
    }


    public synchronized void disconnect () {
        if (!online) return;
        Players.remove(ctx);
        online = false;
        // rooms
    }
}
