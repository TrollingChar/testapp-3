package server.event.server;

import java.nio.ByteBuffer;

/**
 * @author trollingchar
 */
public class UpdateLobbyStatusEvent implements ServerEvent {
    public UpdateLobbyStatusEvent(int size) {
    }

    @Override
    public ByteBuffer getByteBuffer() {
        return null;
    }
}
