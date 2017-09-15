package dto;

import java.nio.ByteBuffer;


/**
 * @author trollingchar
 */
public class EndTurnData implements DTO {
    public boolean alive;
    // here will be checksum


    public EndTurnData (ByteBuffer byteBuffer) {
        deserialize(byteBuffer);
    }


    @Override
    public void serialize (ByteBuffer byteBuffer) {
        byteBuffer.put((byte) (alive ? 1 : 0));
    }


    @Override
    public void deserialize (ByteBuffer byteBuffer) {
        alive = byteBuffer.get() == (byte) 0;
    }
}
