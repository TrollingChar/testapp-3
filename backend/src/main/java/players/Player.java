package players;

import dto.server.ServerCommand;
import io.netty.channel.ChannelHandlerContext;
import rooms.Room;

import java.util.Collection;
import java.util.HashSet;
import java.util.function.Predicate;


public final class Player {

    public final ChannelHandlerContext ctx;
    public final int id;
    private boolean online;
    private Collection<Room> rooms;


    public Player (ChannelHandlerContext ctx, int id) {
        this.ctx = ctx;
        this.id = id;
        rooms = new HashSet<>();
        online = true;
    }


    public final void send (ServerCommand cmd) {
        if (online) ctx.channel().writeAndFlush(cmd);
    }


    public final void disconnect () {
        if (!online) return;
        online = false;
        Players.remove(ctx);
        for (Room room : rooms) room.removePlayer(this);
        rooms.clear();
    }


    public final void joinRoom (Room room) {
        room.addPlayer(this);
        rooms.add(room);
    }


    public final void leaveRoom (Room room) {
        rooms.remove(this);
        room.removePlayer(this);
    }


    public final void onKick (Room room) {
        // called from Room
        rooms.remove(room);
    }


    public final Room getRoom (Predicate<Room> predicate) {
        for (Room room : rooms) if (predicate.test(room)) return room;
        return null;
    }


    public final Collection<Room> getRooms (Room room) {
        return rooms;
    }
}
