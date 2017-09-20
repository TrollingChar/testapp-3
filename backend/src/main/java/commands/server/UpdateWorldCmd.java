package commands.server;

import annotations.ServerCommandCode;
import dto.TurnData;

import java.nio.ByteBuffer;

/**
 * @author trollingchar
 */
@ServerCommandCode(ServerCommandCodes.UPDATE_WORLD)
public class UpdateWorldCmd implements ServerCommand {
    private TurnData turnData;

    public UpdateWorldCmd(TurnData turnData) {
        this.turnData = turnData;
    }

    @Override
    public void serialize(ByteBuffer byteBuffer) {
        turnData.serialize(byteBuffer);
    }
}
