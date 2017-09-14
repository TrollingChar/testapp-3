package commands.server;

import annotations.ServerCommandCode;

import java.nio.ByteBuffer;

@ServerCommandCode(ServerCommandCodes.LOBBY_UPDATED)
public class LobbyUpdatedCmd implements ServerCommand {
    private final byte lobbySize;
    private final byte players;

    public LobbyUpdatedCmd(int lobbySize, int players) {
        this.lobbySize = (byte) lobbySize;
        this.players = (byte) players;
    }

    @Override
    public void serialize(ByteBuffer byteBuffer) {
        byteBuffer.put(lobbySize).put(players);
    }
}
