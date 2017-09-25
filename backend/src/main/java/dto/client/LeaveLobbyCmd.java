package dto.client;

import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;
import rooms.Lobby;


@DTOCode(DTOs.LEAVE_LOBBY)
public class LeaveLobbyCmd extends ClientCommand {

    @Override
    public void execute () {
        Lobby lobby = (Lobby) player.getRoom(room -> room instanceof Lobby);
        if (lobby != null) player.leaveRoom(lobby);
    }


    @Override
    public void readMembers (ByteBuf buffer) {
        // nothing
    }
}
