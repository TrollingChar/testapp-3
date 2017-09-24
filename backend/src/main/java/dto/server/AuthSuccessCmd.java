package dto.server;

import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;


@DTOCode(DTOs.AUTH_SUCCESS)
public class AuthSuccessCmd extends ServerCommand {

    private final int id;


    public AuthSuccessCmd (int id) {
        this.id = id;
    }


    @Override
    protected void writeMembers (ByteBuf byteBuf) {
        byteBuf.writeInt(id);
    }
}
