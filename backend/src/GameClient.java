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
                ClientSocket socket = new ClientSocket();
                // Attempt Connect
                Future<Session> fut = client.connect(socket, uri);
                // Wait for Connect
                Session session = fut.get();
                // Send a message
                ByteBuffer bb = ByteBuffer.allocate(5);
                bb.put(ClientAPI.AUTH);
                bb.putInt(72);
                bb.flip();
                session.getRemote().sendBytes(bb);

                /* Wait then close (toggle /* and //* to change behavior)
                while (ClientSocket.alive) Thread.yield();
                session.close();
                /*/// Close then wait
                session.close();
                while (ClientSocket.alive) Thread.yield();
                //*/
            } finally {
                client.stop();
            }
        } catch (Throwable t) {
            t.printStackTrace(System.err);
        }
    }
}
