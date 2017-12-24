package dto.server;

import dto.DTOCode;
import dto.DTOs;
import io.netty.buffer.ByteBuf;
import players.Player;

import java.util.List;


@DTOCode(DTOs.NEW_GAME)
public class NewGameCmd extends ServerCommand {

    private final int seed;
    private final List<Player> players;


    public NewGameCmd (int seed, List<Player> players) {
        this.seed = seed;
        this.players = players;
    }


    @Override
    public void writeMembers (ByteBuf byteBuf) {
        byteBuf
            .writeInt(seed)//0x23d18203)
            .writeByte(players.size());

        for (Player player : players) byteBuf.writeInt(player.id);
    }
}
