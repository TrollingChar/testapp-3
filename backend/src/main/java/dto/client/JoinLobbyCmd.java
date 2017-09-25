package dto.client;

import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;
import rooms.Lobbies;


@DTOCode(DTOs.JOIN_LOBBY)
public class JoinLobbyCmd extends ClientCommand {

    private byte lobbySize;


    @Override
    public void execute () {
        player.joinRoom(Lobbies.get(lobbySize));
    }


    @Override
    public void readMembers (ByteBuf buffer) {
        lobbySize = buffer.readByte();
    }
}
