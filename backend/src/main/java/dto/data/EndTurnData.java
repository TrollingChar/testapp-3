package dto.data;

import dto.DTO;
import io.netty.buffer.ByteBuf;


public class EndTurnData extends DTO {

    public boolean alive;


    @Override
    public void writeMembers (ByteBuf byteBuf) {
        byteBuf.writeBoolean(alive);
    }


    @Override
    public void readMembers (ByteBuf buffer) {
        alive = buffer.readBoolean();
    }
}
