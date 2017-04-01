import org.eclipse.jetty.websocket.api.Session;

import java.io.IOException;
import java.nio.ByteBuffer;
import java.util.LinkedList;

public class Player {
    Session session;
    public int id;
    public byte hub;
    public Room room;

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

    public void sendConfirmCancel() throws IOException {
        ByteBuffer bb = ByteBuffer.allocate(1);
        bb.put(ServerAPI.CONFIRM_CANCEL);
        bb.flip();
        session.getRemote().sendBytes(bb);
    }

    public void sendStartGame(LinkedList<Player> players) throws IOException {
        ByteBuffer bb = ByteBuffer.allocate(2 + 4*players.size());
        bb.put(ServerAPI.START_GAME);
        bb.put((byte)players.size());
        for (Player player : players) bb.putInt(player.id);
        session.getRemote().sendBytes(bb);
    }
}
