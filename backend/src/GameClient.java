import java.net.URI;
import java.nio.ByteBuffer;
import java.util.concurrent.Future;

import org.eclipse.jetty.websocket.api.Session;
import org.eclipse.jetty.websocket.client.WebSocketClient;

public class GameClient {
    public static void main(String[] args) {
        URI uri = URI.create("ws://localhost:8080/events/");

        WebSocketClient client = new WebSocketClient();
        try {
            try {
                client.start();
                // The socket that receives events
                GameSocket socket = new GameSocket();
                // Attempt Connect
                Future<Session> fut = client.connect(socket, uri);
                // Wait for Connect
                Session session = fut.get();
                for (; ; ) {
                    // Send a message
                    ByteBuffer bb = ByteBuffer.allocate(1);
                    session.getRemote().sendBytes(bb);
                }
                // Close session
                //session.close();
            } finally {
                client.stop();
            }
        } catch (Throwable t) {
            t.printStackTrace(System.err);
        }
    }
}
