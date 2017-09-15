package commands.client;

import annotations.ClientCommandCode;
import dto.TurnData;
import rooms.Battle;

import java.nio.ByteBuffer;


/**
 * @author trollingchar
 */
@ClientCommandCode(ClientCommandCodes.TURN_DATA)
public class TurnDataCmd extends ClientCommand {
    private TurnData turnData;


    @Override
    public void deserialize (ByteBuffer byteBuffer) {
        turnData = new TurnData(byteBuffer);
    }


    @Override
    public void execute () {
        Battle battle = (Battle) player.getRoom(room -> room instanceof Battle);
        if (battle != null) battle.onTurnData(player, turnData);
    }
}
