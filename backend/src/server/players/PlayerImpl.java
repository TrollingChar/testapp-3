package server.players;

import org.eclipse.jetty.websocket.api.Session;
import server.Hub;
import server.Hubs;
import server.Room;
import server.ServerAPI;
import server.event.server.ServerEvent;
import util.GameLogger;

import java.io.IOException;
import java.nio.ByteBuffer;

public class PlayerImpl implements Player {

    private boolean dead;

    private Session session;
    private int id;

    public Hub hub;
    public Room room;

    public PlayerImpl(Session session, int id) {
        this.session = session;
        this.id = id;
    }

    @Override
    public void send(ServerEvent cmd) {
        // synchronize?
        if (dead) return;
        try {
            send(cmd.getByteBuffer());
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @Override
    public int getId() {
        return dead ? -1 : id;
    }

    @Override
    public void disconnect() {
        if (dead) return;

        if (room != null) room.remove(this);
        if (hub != null) hub.removeSilently(this);

        Players.remove(this);
        session.close();
    }

    @Deprecated
    public void sendAccountData() throws IOException {
        System.out.println("PlayerImpl.sendAccountData");
        send(ByteBuffer.allocate(5)
        .put(ServerAPI.ACCOUNT_DATA)
        .putInt(id));
    }

    @Deprecated
    public void switchHub(byte hubId) throws IOException {
        System.out.println("PlayerImpl.switchHub");
        if (hubId == 0) {
            // remove from hub
            if (hub != null) hub.remove(this);
        } else {
            // remove if in hub and add to another
            if (hub != null) hub.removeSilently(this);
            Hubs.add(this, hubId);
        }
    }

    @Deprecated
    public void sendHubStatus(int hubId, int players) throws IOException {
        System.out.println("PlayerImpl.sendHubStatus");
        send(ByteBuffer.allocate(3)
        .put(ServerAPI.HUB_STATUS)
        .put((byte) hubId)
        .put((byte) players));
    }

    public void send(ByteBuffer bb) throws IOException {
        System.out.println("PlayerImpl.send");
        bb.flip();
        System.out.println("#" + id + " <-" + GameLogger.str(bb));
        session.getRemote().sendBytes(bb);
    }
}
