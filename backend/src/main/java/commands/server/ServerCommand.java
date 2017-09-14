package commands.server;

import java.nio.ByteBuffer;

public interface ServerCommand {
    default void serialize(ByteBuffer byteBuffer) {}
}
