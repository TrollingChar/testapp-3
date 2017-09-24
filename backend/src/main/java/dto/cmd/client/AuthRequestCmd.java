package dto.cmd.client;

import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;


@DTOCode(DTOs.AUTH_REQUEST)
public class AuthRequestCmd extends ClientCommand {

    public int id;


    @Override
    public void execute () {
        // todo: remove a player with same id if exists
    }


    @Override
    protected void readMembers (ByteBuf buffer) throws Exception {
        id = buffer.readInt();
    }
}
