import org.eclipse.jetty.websocket.api.Session;

import java.io.IOException;
import java.nio.ByteBuffer;
import java.util.Collection;
import java.util.Queue;

public class Player {

    private final int id;
    private final Session session;
    public Hub hub;
    public Room room;

    public Player(Session session, int id) {
        this.session = session;
        this.id = id;
    }

    public void disconnect() {
        Lookup.removePlayer(session, id);
        if(room != null) room.remove(this);
        session.close();
    }

    public void moveToHub(Hub hub, int hubId) throws IOException {
        if(this.hub != null) this.hub.remove(this);
        this.hub = null;
        sendHubChanged(hubId);
        // if exception, do not add player to hub
        this.hub = hub;
        if(hub != null) hub.add(this);
    }

    public void quitRoom() {
        // need to deal with situation when player surrenders
        // when battle room is no longer exist
        // so we don't throw exception which cause disconnect
        if(room != null) room.remove(this);
    }

    public void doTurn(ByteBuffer data) {
        room.doTurn(this, data);
    }

    public void endTurn(int frames, int hash) {
        room.endTurn(this, frames, hash);
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

    public void sendStartGame(byte[] bytes, Collection<Player> players) throws IOException {
        ByteBuffer bb = ByteBuffer.allocate(2 + bytes.length + players.size() * 4);
        bb.put(ServerAPI.START_GAME);
        bb.put(bytes);
        bb.put((byte)players.size());
        for (Player player : players) bb.putInt(player.id);
        bb.flip();
        session.getRemote().sendBytes(bb);
    }

    public void sendPlayersLeft(Collection<Player> players) throws IOException {
        ByteBuffer bb = ByteBuffer.allocate(2 + players.size() * 4);
        bb.put(ServerAPI.LEFT_GAME);
        bb.put((byte)players.size());
        for (Player player : players) bb.putInt(player.id);
        bb.flip();
        session.getRemote().sendBytes(bb);
    }

    public void sendPlayerLeft(Player player) throws IOException {
        ByteBuffer bb = ByteBuffer.allocate(6);
        bb.put(ServerAPI.LEFT_GAME);
        bb.put((byte)1);
        bb.putInt(player.id);
        bb.flip();
        session.getRemote().sendBytes(bb);
    }

    public void sendWin() throws IOException {
        ByteBuffer bb = ByteBuffer.allocate(1);
        bb.put(ServerAPI.YOU_WIN);
        bb.flip();
        session.getRemote().sendBytes(bb);
    }

    public void sendTurnData(ByteBuffer data) {
        ByteBuffer bb = ByteBuffer.allocate(10);
        bb.put(ServerAPI.TURN_DATA);
        bb.put(data);
        data.position(0);
    }
}
