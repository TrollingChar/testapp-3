package dto.cmd.server;

import dto.DTO;
import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;


@DTOCode(DTOs.AUTH_SUCCESS)
public class AuthSuccessCmd extends DTO {

    @Override
    protected void writeMembers (ByteBuf byteBuf) {

    }


    @Override
    protected void readMembers (ByteBuf buffer) {

    }
}
