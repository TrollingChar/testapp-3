package util;

import java.nio.ByteBuffer;


public class ByteBufferLogger {

    private ByteBufferLogger () {
        // no instance
    }


    public static void log (ByteBuffer byteBuffer) {
        int pos = byteBuffer.position();
        StringBuilder s = new StringBuilder("bytes:");
        while (byteBuffer.hasRemaining()) s.append(String.format(" %02x", byteBuffer.get()));
        System.out.println(s);
        byteBuffer.position(pos);
    }
}
