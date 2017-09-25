package dto.client;

import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;
import rooms.Game;
import rooms.Room;


@DTOCode(DTOs.LEAVE_GAME)
public class LeaveGameCommand extends ClientCommand {

    @Override
    public void execute () {
        Room game = player.getRoom(room -> room instanceof Game);
        if (game != null) player.leaveRoom(game);
    }


    @Override
    public void readMembers (ByteBuf buffer) {
        // nothing
    }
}
