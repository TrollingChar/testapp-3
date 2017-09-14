package commands.client;

import annotations.ClientCommandCode;
import rooms.Battle;
import rooms.Room;

/**
 * @author trollingchar
 */
@ClientCommandCode(ClientCommandCodes.LEAVE_GAME)
public class LeaveGameCmd extends ClientCommand {
    @Override
    public void execute() {
        Room battle = player.getRoom(room -> room instanceof Battle);
        if (battle != null) player.quitRoom(battle);
    }
}
