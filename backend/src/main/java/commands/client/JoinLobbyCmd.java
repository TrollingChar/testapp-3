package commands.client;

import annotations.ClientCommandCode;
import rooms.Lobbies;

import java.nio.ByteBuffer;


@ClientCommandCode(ClientCommandCodes.JOIN_LOBBY)
public class JoinLobbyCmd extends ClientCommand {

    private byte lobbyId;


    @Override
    public void deserialize (ByteBuffer byteBuffer) {
        lobbyId = byteBuffer.get();
    }


    @Override
    public void execute () {
        player.joinRoom(Lobbies.getLobby(lobbyId));
    }
}
