package dto.client;

import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;
import players.Player;
import players.Players;


@DTOCode(DTOs.AUTH_REQUEST)
public class AuthRequestCmd extends ClientCommand {

    public int id;


    @Override
    public void execute () {
        player = Players.get(id);
        if (player != null) player.disconnect();
    }


    @Override
    public void readMembers (ByteBuf buffer) throws Exception {
        id = buffer.readInt();
    }
}
