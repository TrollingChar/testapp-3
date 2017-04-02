import org.eclipse.jetty.websocket.api.Session;

import java.io.IOException;
import java.util.LinkedList;
import java.util.Queue;

public class HQ {
    static Hub[] hubs = new Hub[256];

    public static void init() {
        for (int i = 1; i < 256; ++i) hubs[i] = new Hub(i);
    }

    public static void movePlayerToHub(Player player, int hub) throws IOException {
        player.moveToHub(hubs[hub], hub);
    }

    public static void disconnect(Player player) {
        player.disconnect();
    }
}