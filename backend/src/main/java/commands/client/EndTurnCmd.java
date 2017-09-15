package commands.client;

import annotations.ClientCommandCode;
import dto.EndTurnData;
import rooms.Battle;

import java.nio.ByteBuffer;


/**
 * @author trollingchar
 */
@ClientCommandCode(ClientCommandCodes.END_TURN)
public class EndTurnCmd extends ClientCommand {
    private EndTurnData endTurnData;


    @Override
    public void deserialize (ByteBuffer byteBuffer) {
        endTurnData = new EndTurnData(byteBuffer);
    }


    @Override
    public void execute () {
        Battle battle = (Battle) player.getRoom(room -> room instanceof Battle);
        if (battle != null) battle.onEndTurn(player, endTurnData);
    }
}
