package dto.cmd.server;

import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;


@DTOCode(DTOs.LOBBY_STATUS)
public class LobbyStatusCmd extends ServerCommand {

    private final int lobbySize;
    private final int playersCount;


    public LobbyStatusCmd (int lobbySize, int playersCount) {

        this.lobbySize = lobbySize;
        this.playersCount = playersCount;
    }


    @Override
    protected void writeMembers (ByteBuf byteBuf) {

    }
}
