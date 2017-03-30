import io.netty.buffer.ByteBuf;
import io.netty.buffer.Unpooled;
import io.netty.channel.ChannelHandlerContext;

import java.util.HashMap;
import java.util.Map;

public class HQ {
    public static Map<ChannelHandlerContext, Player> players = new HashMap<>();
    public static Map<Integer, Player> playersById = new HashMap<>();

    static Player getPlayer(ChannelHandlerContext ctx) {
        return players.get(ctx);
    }

    static Player getPlayer(int id) {
        return playersById.get(id);
    }

    public static void receiveAuth(ChannelHandlerContext ctx, int id) {
        Player player = getPlayer(id);
        if (player != null) {
            sendIdInUse(ctx);
            ctx.close();
            return;
        }
        // success:
        player = new Player(ctx, id);
        players.put(ctx, player);
        playersById.put(id, player);
        player.sendAccountData();
    }

    public static void receiveStartGame(ChannelHandlerContext ctx, int numPlayers) {
        getPlayer(ctx).receiveStartGame();
    }

    public static void receiveCancel(ChannelHandlerContext ctx) {
        getPlayer(ctx).receiveCancel();
    }

    public static void receiveSync(ChannelHandlerContext ctx) {
        getPlayer(ctx).receiveSync();
    }

    public static void receiveTurnData(ChannelHandlerContext ctx, ByteBuf buf) {
        getPlayer(ctx).receiveTurnData();
    }

    public static void receiveQuitGame(ChannelHandlerContext ctx) {
        getPlayer(ctx).receiveQuitGame();
    }

    public static void receiveDisconnect(ChannelHandlerContext ctx) {
        Player player = getPlayer(ctx);
        if (player == null) {
            ctx.close();
        } else {
            disconnect(player);
        }
    }

    private static void disconnect(Player player) {
        players.remove(player.ctx);
        playersById.remove(player.id);
        player.ctx.close();
    }

    public static void receiveCheat(ChannelHandlerContext ctx) { }

    private static void sendIdInUse(ChannelHandlerContext ctx) {
        ByteBuf buffer = Unpooled.buffer();
        buffer.writeByte(ServerAPI.ID_IN_USE);
    }
}
