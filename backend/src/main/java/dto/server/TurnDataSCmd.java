package dto.server;

import dto.DTOCode;
import dto.DTOs;
import dto.data.TurnData;
import io.netty.buffer.ByteBuf;


@DTOCode(DTOs.TURN_DATA_SERVER)
public class TurnDataSCmd extends ServerCommand {

    private final TurnData data;


    public TurnDataSCmd (TurnData data) {
        this.data = data;
    }


    @Override
    public void writeMembers (ByteBuf byteBuf) {
        data.writeMembers(byteBuf);
    }
}
