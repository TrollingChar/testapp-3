package commands.server;

import annotations.ServerCommandCode;

import java.nio.ByteBuffer;

@ServerCommandCode(ServerCommandCodes.AUTHORIZED)
public class AuthorizedCmd implements ServerCommand {

    private final int playerId;

    public AuthorizedCmd(int playerId) {
        this.playerId = playerId;
    }

    @Override
    public void serialize(ByteBuffer byteBuffer) {
        byteBuffer.putInt(playerId);
    }
}
