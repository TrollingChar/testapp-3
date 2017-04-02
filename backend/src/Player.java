import org.eclipse.jetty.websocket.api.Session;

import java.io.IOException;
import java.nio.ByteBuffer;
import java.util.Queue;

public class Player {

    private final int id;
    private final Session session;
    public Hub hub;

    public Player(Session session, int id) {
        this.session = session;
        this.id = id;
    }

    public void disconnect() {
        Lookup.removePlayer(session, id);
        session.close();
    }

    public void moveToHub(Hub hub, int hubId) throws IOException {
        if(this.hub != null) this.hub.remove(this);
        this.hub = null;
        // if exception, do not add player to hub
        sendHubChanged(hubId);
        this.hub = hub;
        if(hub != null) hub.add(this);
    }

    public void sendAccountData() throws IOException {
        ByteBuffer bb = ByteBuffer.allocate(1);
        bb.put(ServerAPI.ACCOUNT_DATA);
        bb.flip();
        session.getRemote().sendBytes(bb);
    }

    private void sendHubChanged(int hubId) throws IOException {
        ByteBuffer bb = ByteBuffer.allocate(2);
        bb.put(ServerAPI.HUB_CHANGED);
        bb.put((byte)hubId);
        bb.flip();
        session.getRemote().sendBytes(bb);
    }

    public void sendStartGame(byte[] bytes, Queue<Player> players) throws IOException {
        ByteBuffer bb = ByteBuffer.allocate(2 + bytes.length + players.size() * 4);
        bb.put(ServerAPI.START_GAME);
        bb.put(bytes);
        bb.put((byte)players.size());
        for (Player player : players) bb.putInt(player.id);
        bb.flip();
        session.getRemote().sendBytes(bb);
    }

    public void sendPlayersLeft(Queue<Player> players) throws IOException {
        ByteBuffer bb = ByteBuffer.allocate(2 + players.size() * 4);
        bb.put(ServerAPI.LEFT_GAME);
        bb.put((byte)players.size());
        for (Player player : players) bb.putInt(player.id);
        bb.flip();
        session.getRemote().sendBytes(bb);
    }
}
