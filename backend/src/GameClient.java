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

                // authorize as random user
                ByteBuffer bb = ByteBuffer.allocate(5);
                bb.put(ClientAPI.AUTH);
                bb.putFloat((float)Math.random()); // hack
                bb.flip();
                session.getRemote().sendBytes(bb);
                // wait
                Thread.sleep(2500);
                // send signal to start game
                bb = ByteBuffer.allocate(2);
                bb.put(ClientAPI.TO_HUB);
                bb.put((byte)2);
                bb.flip();
                session.getRemote().sendBytes(bb);

                //* Wait then close (toggle /* and //* to change behavior)
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
