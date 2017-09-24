package dto.client;

import dto.DTOCode;
import dto.DTOs;
import dto.data.TurnData;
import io.netty.buffer.ByteBuf;
import rooms.Game;


@DTOCode(DTOs.TURN_DATA_CLIENT)
public class TurnDataCCmd extends ClientCommand {

    private TurnData data;


    @Override
    public void execute () {
        Game game = (Game) player.getRoom(room -> room instanceof Game);
        game.processTurnData(player, data);
    }


    @Override
    protected void readMembers (ByteBuf buffer) {
        data = new TurnData();
        data.readMembers(buffer);
    }
}
