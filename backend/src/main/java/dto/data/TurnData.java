package dto.data;

import dto.DTO;
import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;


@DTOCode(DTOs.TURN_DATA)
public class TurnData extends DTO {

    private byte keys;
    private float x, y;
    private byte weaponId;
    private byte numKey;


    @Override
    public void writeMembers (ByteBuf byteBuf) {
        byteBuf
            .writeByte(keys)
            .writeFloat(x)
            .writeFloat(y)
            .writeByte(weaponId)
            .writeByte(numKey);
    }


    @Override
    public void readMembers (ByteBuf buffer) {
        keys = buffer.readByte();
        x = buffer.readFloat();
        y = buffer.readFloat();
        weaponId = buffer.readByte();
        numKey = buffer.readByte();
    }
}
