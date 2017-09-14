package commands.server;

import annotations.ServerCommandCode;

import java.nio.ByteBuffer;

/**
 * @author trollingchar
 */
@ServerCommandCode(ServerCommandCodes.GAME_ENDED)
public class GameEndedCmd implements ServerCommand {

    private static final byte
            DESYNC = -1,
            VICTORY = 0,
            DRAW = 1;

    private byte result;
    private int playerId;

    private GameEndedCmd() {
    }

    public static ServerCommand victory(int winner) {
        GameEndedCmd cmd = new GameEndedCmd();
        cmd.result = VICTORY;
        cmd.playerId = winner;
        return cmd;
    }

    public static ServerCommand draw() {
        GameEndedCmd cmd = new GameEndedCmd();
        cmd.result = DRAW;
        return cmd;
    }

    public static ServerCommand desync() {
        GameEndedCmd cmd = new GameEndedCmd();
        cmd.result = DESYNC;
        return cmd;
    }

    @Override
    public void serialize(ByteBuffer byteBuffer) {
        byteBuffer.put(result);
        if (result == VICTORY) byteBuffer.putInt(playerId);
    }
}
