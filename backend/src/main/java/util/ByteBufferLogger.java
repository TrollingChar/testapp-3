package util;

import java.nio.ByteBuffer;


public class ByteBufferLogger {

    private ByteBufferLogger () {
        // no instance
    }


    public static String str (ByteBuffer byteBuffer) {
        int pos = byteBuffer.position();
        StringBuilder s = new StringBuilder("bytes:");
        while (byteBuffer.hasRemaining()) s.append(String.format(" %02x", byteBuffer.get()));
        byteBuffer.position(pos);
        return s.toString();
    }
}
