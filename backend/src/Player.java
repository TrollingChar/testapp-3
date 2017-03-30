import io.netty.buffer.ByteBuf;
import io.netty.buffer.Unpooled;
import io.netty.channel.ChannelHandlerContext;

import java.nio.ByteOrder;

public class Player {
    ChannelHandlerContext ctx;
    int id;
    Room room;

    public Player(ChannelHandlerContext ctx, int id) {
        this.ctx = ctx;
        this.id = id;
    }

    public void receiveStartGame() {
    }

    public void receiveCancel() {
    }

    public void receiveSync() {
    }

    public void receiveTurnData() {
    }

    public void receiveQuitGame() {
    }

    public void receiveCheat() {
    }

    public void sendAccountData() {
        int supply, intel, xp, stars;
        supply = intel = xp = stars = 72;

        ByteBuf buffer = Unpooled.buffer();
        buffer.writeByte(ServerAPI.ACCOUNT_DATA);
        buffer.writeByte(0);
        buffer.writeInt(supply);
        buffer.writeInt(intel);
        buffer.writeInt(xp);
        buffer.writeInt(stars);
        ctx.writeAndFlush(buffer);
    }

    public void sendConfirmCancel() {
        //ctx.writeByte(ServerAPI.CONFIRM_CANCEL);
        //ctx.flush();
    }

    public void sendStartTurn(int player) {
        //ctx.writeByte(ServerAPI.START_TURN);
        //ctx.writeInt(player);
        //ctx.flush();
    }

    public void sendPlayerLeft() {
    }
}
