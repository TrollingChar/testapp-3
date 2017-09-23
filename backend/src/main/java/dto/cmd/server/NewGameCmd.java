package dto.cmd.server;

import dto.DTO;
import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;


@DTOCode(DTOs.NEW_GAME)
public class NewGameCmd extends DTO {

    @Override
    protected void writeMembers (ByteBuf byteBuf) {

    }


    @Override
    protected void readMembers (ByteBuf buffer) {

    }
}
