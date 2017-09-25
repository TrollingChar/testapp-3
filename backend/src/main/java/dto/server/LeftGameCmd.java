package dto.server;

import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;


@DTOCode(DTOs.LEFT_GAME)
public class LeftGameCmd extends ServerCommand {

    private int id;


    public LeftGameCmd (int id) {
        this.id = id;
    }


    @Override
    public void writeMembers (ByteBuf byteBuf) {
        byteBuf.writeInt(id);
    }
}
