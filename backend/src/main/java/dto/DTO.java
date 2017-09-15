package dto;

import java.nio.ByteBuffer;


/**
 * @author trollingchar
 */
public interface DTO {
    void serialize (ByteBuffer byteBuffer);
    void deserialize (ByteBuffer byteBuffer);
}
