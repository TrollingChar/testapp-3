package util;

import java.nio.ByteBuffer;

/**
 * Created by Дима on 02.04.2017.
 */
public class GameLogger {

    public static void println(ByteBuffer bb) {
        int pos = bb.position();
        String s = "bytes:";
        while (bb.hasRemaining()) s += String.format(" %02x", bb.get());
        System.out.println(s);
        bb.position(pos);
    }
}
