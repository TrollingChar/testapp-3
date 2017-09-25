package dto.server;

import dto.DTO;
import io.netty.buffer.ByteBuf;


public abstract class ServerCommand extends DTO {

    @Override
    public final void readMembers (ByteBuf buffer) throws Exception {
        throw new Exception("attempt to deserialize server command");
    }
}
