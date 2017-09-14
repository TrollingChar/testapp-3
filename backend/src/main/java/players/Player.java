package players;

import annotations.ServerCommandCode;
import commands.server.ServerCommand;
import org.eclipse.jetty.websocket.api.Session;
import rooms.Room;

import java.io.IOException;
import java.nio.ByteBuffer;
import java.util.HashSet;
import java.util.Optional;
import java.util.Set;
import java.util.function.Predicate;

public class Player {
    private final int id;
    private boolean online;
    private Session session;
    private Set<Room> rooms;

    public Player(int id, Session session) {
        online = true;
        rooms = new HashSet<>();

        this.id = id;
        this.session = session;
    }

    public void send(ServerCommand cmd) {
        if (!online) return;

        ByteBuffer bb = ByteBuffer.allocate(64);
        bb.put(cmd.getClass().getAnnotation(ServerCommandCode.class).value());
        cmd.serialize(bb);
        bb.flip();
        try {
            session.getRemote().sendBytes(bb);
        } catch (IOException e) {
            online = false;
            System.err.println("ERROR WHEN SENDING DATA TO THE PLAYER");
            disconnect();
        }
    }

    public void disconnect() {
        if (online) try {
            session.disconnect();
        } catch (IOException e) {
            System.err.println("ERROR WHEN DISCONNECTING PLAYER");
        }
        online = false;
        // notify rooms
        for (Room room : rooms) room.removePlayer(this);
        Players.logout(id);
    }

    public void joinRoom(Room room) {
        rooms.add(room);
        room.addPlayer(this);
    }

    public void quitRoom(Room room) {
        rooms.remove(room);
        room.removePlayer(this);
    }

    public Room getRoom(Predicate<Room> predicate) {
        for (Room room : rooms) if (predicate.test(room)) return room;
        return null;
    }

    public Set<Room> getRooms() {
        return rooms;
    }

    public void log(String msg) {
        System.out.printf("player %d : %s\n", id, msg);
    }

    public void logCheating(String msg) {
        System.out.printf("player %d : cheating attempt : %s\n", id, msg);
    }

    public int getId() {
        return id;
    }
}
