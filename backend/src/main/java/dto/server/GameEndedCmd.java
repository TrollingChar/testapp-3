package dto.server;

import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;
import players.Player;


@DTOCode(DTOs.GAME_ENDED)
public class GameEndedCmd extends ServerCommand {

    private static final byte DRAW = 0;
    private static final byte VICTORY = 1;
    private static final byte DESYNC = -1;

    private final byte result;
    private final int playerId;

    @Override
    public void writeMembers (ByteBuf byteBuf) {
        byteBuf.writeByte(result);
        if (result == VICTORY) byteBuf.writeInt(playerId);
    }


    private GameEndedCmd (byte result, int playerId) {
        this.result = result;
        this.playerId = playerId;
    }


    public static GameEndedCmd draw () {
        return new GameEndedCmd(DRAW, 0);
    }


    public static GameEndedCmd victory (Player winner) {
        return new GameEndedCmd(VICTORY, winner.id);
    }
}
