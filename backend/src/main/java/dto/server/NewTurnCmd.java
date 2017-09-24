package dto.server;

import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;


@DTOCode(DTOs.NEW_TURN)
public class NewTurnCmd extends ServerCommand {

    @Override
    protected void writeMembers (ByteBuf byteBuf) {

    }
}
