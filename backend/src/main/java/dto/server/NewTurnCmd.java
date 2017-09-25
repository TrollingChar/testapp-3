package dto.server;

import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;


@DTOCode(DTOs.NEW_TURN)
public class NewTurnCmd extends ServerCommand {

    private final int playerId;


    public NewTurnCmd (int playerId) {
        this.playerId = playerId;
    }


    @Override
    public void writeMembers (ByteBuf byteBuf) {
        byteBuf.writeInt(playerId);
    }
}
