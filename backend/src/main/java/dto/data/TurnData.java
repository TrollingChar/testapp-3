package dto.data;

import dto.DTO;
import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;


@DTOCode(DTOs.TURN_DATA)
public class TurnData extends DTO {

    @Override
    public void writeMembers (ByteBuf byteBuf) {

    }


    @Override
    public void readMembers (ByteBuf buffer) {

    }
}
