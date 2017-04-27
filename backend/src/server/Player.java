package server;

import org.eclipse.jetty.websocket.api.Session;

import java.io.IOException;
import java.nio.ByteBuffer;

public class Player {
    public Session session;
    public int id;
    public Hub hub;
    public Room room;

    public Player(Session session, int id) {
        System.out.println("Player.Player");
        this.session = session;
        this.id = id;
    }

    public void disconnect() {
        System.out.println("Player.disconnect");
        if (room != null) room.remove(this);
        if (hub != null) hub.removeSilently(this);
        Players.remove(this);
        session.close();
    }

    public void sendAccountData() throws IOException {
        System.out.println("Player.sendAccountData");
        send(ByteBuffer.allocate(1)
        .put(ServerAPI.ACCOUNT_DATA));
    }

    public void switchHub(byte hubId) throws IOException {
        System.out.println("Player.switchHub");
        if (hubId == 0) {
            // remove from hub
            if (hub != null) hub.remove(this);
        } else {
            // remove if in hub and add to another
            if (hub != null) hub.removeSilently(this);
            Hubs.add(this, hubId);
        }
    }

    public void sendHubStatus(int hubId, int players) throws IOException {
        System.out.println("Player.sendHubStatus");
        send(ByteBuffer.allocate(3)
        .put(ServerAPI.HUB_STATUS)
        .put((byte) hubId)
        .put((byte) players));
    }

    public void send(ByteBuffer bb) throws IOException {
        System.out.println("Player.send");
        bb.flip();
        session.getRemote().sendBytes(bb);
    }
}
