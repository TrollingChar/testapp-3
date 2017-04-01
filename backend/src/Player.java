import org.eclipse.jetty.websocket.api.Session;

import java.io.IOException;
import java.nio.ByteBuffer;

/**
 * Created by Дима on 31.03.2017.
 */
public class Player {
    Session session;
    public int id;

    public Player(Session session, int id) {
        this.session = session;
        this.id = id;
    }

    public void sendAccountData() throws IOException {
        ByteBuffer bb = ByteBuffer.allocate(1);
        bb.put(ServerAPI.ACCOUNT_DATA);
        bb.flip();
        session.getRemote().sendBytes(bb);
    }
}
