package commands.server;

import annotations.ServerCommandCode;

import java.nio.ByteBuffer;

/**
 * @author trollingchar
 */
@ServerCommandCode(ServerCommandCodes.NEW_TURN)
public class NewTurnCmd implements ServerCommand {
    private int playerId;

    public NewTurnCmd(int playerId) {
        this.playerId = playerId;
    }

    @Override
    public void serialize(ByteBuffer byteBuffer) {
        byteBuffer.putInt(playerId);
    }
}
