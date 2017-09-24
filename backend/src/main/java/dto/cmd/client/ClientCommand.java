package dto.cmd.client;

import dto.DTO;
import io.netty.buffer.ByteBuf;
import players.Player;


public abstract class ClientCommand extends DTO {

    public Player player;


    public abstract void execute ();


    @Override
    protected void writeMembers (ByteBuf byteBuf) throws Exception {
        throw new Exception("attempt to serialize client command");
    }
}
