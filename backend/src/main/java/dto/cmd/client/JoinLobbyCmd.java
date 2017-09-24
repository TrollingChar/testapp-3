package dto.cmd.client;

import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;


@DTOCode(DTOs.JOIN_LOBBY)
public class JoinLobbyCmd extends ClientCommand {

    @Override
    public void execute () {

    }


    @Override
    protected void writeMembers (ByteBuf byteBuf) {

    }


    @Override
    protected void readMembers (ByteBuf buffer) {

    }
}
