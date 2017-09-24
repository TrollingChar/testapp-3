package dto.client;

import dto.DTOCode;
import dto.DTOs;
import dto.data.EndTurnData;
import io.netty.buffer.ByteBuf;
import rooms.Game;


@DTOCode(DTOs.TURN_ENDED)
public class TurnEndedCmd extends ClientCommand {

    private EndTurnData data;


    @Override
    public void execute () {
        Game game = (Game) player.getRoom(room -> room instanceof Game);
        game.processEndTurn(player, data);
    }


    @Override
    protected void readMembers (ByteBuf buffer) {

    }
}
