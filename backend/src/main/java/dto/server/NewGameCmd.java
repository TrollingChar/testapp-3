package dto.server;

import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;
import players.Player;

import java.util.List;


@DTOCode(DTOs.NEW_GAME)
public class NewGameCmd extends ServerCommand {

    public NewGameCmd (int seed, List<Player> players) {
        // todo
    }


    @Override
    public void writeMembers (ByteBuf byteBuf) {

    }
}
