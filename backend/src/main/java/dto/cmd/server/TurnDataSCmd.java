package dto.cmd.server;

import dto.DTO;
import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;


@DTOCode(DTOs.TURN_DATA_SERVER)
public class TurnDataSCmd extends DTO {

    @Override
    protected void writeMembers (ByteBuf byteBuf) {

    }


    @Override
    protected void readMembers (ByteBuf buffer) {

    }
}
