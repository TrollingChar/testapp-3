package dto;

import java.nio.ByteBuffer;


/**
 * @author trollingchar
 */
public class TurnData implements DTO {
    private byte keys; // w, a, s, d, lmb, tab, space...
    private float x, y;
    private byte weapon; // 0 - not selected
    private byte numKey; // 1-5, 0 - not pressed


    public TurnData (ByteBuffer byteBuffer) {
        deserialize(byteBuffer);
    }


    @Override
    public void serialize (ByteBuffer byteBuffer) {
        byteBuffer
            .put(keys)
            .putFloat(x)
            .putFloat(y)
            .put(weapon)
            .put(numKey);
    }


    @Override
    public void deserialize (ByteBuffer byteBuffer) {
        keys = byteBuffer.get();
        x = byteBuffer.getFloat();
        y = byteBuffer.getFloat();
        weapon = byteBuffer.get();
        numKey = byteBuffer.get();
    }
}
