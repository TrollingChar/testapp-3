package dto.server;

import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;


@DTOCode(DTOs.GAME_ENDED)
public class GameEndedCmd extends ServerCommand {

    @Override
    public void writeMembers (ByteBuf byteBuf) {

    }
}
