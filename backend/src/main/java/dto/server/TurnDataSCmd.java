package dto.server;

import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;


@DTOCode(DTOs.TURN_DATA_SERVER)
public class TurnDataSCmd extends ServerCommand {

    @Override
    protected void writeMembers (ByteBuf byteBuf) {

    }
}
