package dto.server;

import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;


@DTOCode(DTOs.LEFT_LOBBY)
public class LeftLobbyCmd extends ServerCommand {

    @Override
    protected void writeMembers (ByteBuf byteBuf) {
        // nothing
    }
}
