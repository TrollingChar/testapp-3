package server.core;

import org.eclipse.jetty.websocket.api.Session;
import org.eclipse.jetty.websocket.api.WebSocketAdapter;
import server.players.PlayerImpl;
import server.players.Players;
import server.SyncData;
import server.TurnData;
import util.ClientAPI;
import util.GameLogger;

import java.nio.ByteBuffer;

public class ServerSocket extends WebSocketAdapter {

    @Override
    public void onWebSocketConnect(Session session) {
        System.out.println("ServerSocket.onWebSocketConnect");
        super.onWebSocketConnect(session);
    }

    @Override
    public void onWebSocketText(String message) {
        System.out.println("ServerSocket.onWebSocketText");
        super.onWebSocketText(message);
        Session session = getSession();
        // text not allowed
        try {
            Players.get(session).disconnect();
        } catch (Throwable e) {
            session.close();
        }
    }

    @Override
    public void onWebSocketClose(int statusCode, String reason) {
        System.out.println("ServerSocket.onWebSocketClose");
        try {
            Players.get(getSession()).disconnect();
        } catch (Throwable e) {
            System.err.println(e.getMessage());
            e.printStackTrace();
        }
        super.onWebSocketClose(statusCode, reason);
        System.out.println("connection closed");
    }

    @Override
    public void onWebSocketError(Throwable cause) {
        System.out.println("ServerSocket.onWebSocketError");
        super.onWebSocketError(cause);
        cause.printStackTrace(System.err);
    }

    @Override
    public void onWebSocketBinary(byte[] payload, int offset, int len) {
        System.out.println("ServerSocket.onWebSocketBinary");
        super.onWebSocketBinary(payload, offset, len);

        ByteBuffer bb = ByteBuffer.wrap(payload, offset, len);
        GameLogger.println(bb);
        if (len <= 0) return;

        Session session = getSession();
        PlayerImpl player = Players.get(session);
        try {
            // todo: replace it with commands
            switch (bb.get()) {
                case ClientAPI.AUTH:
                    // Players singleton subscribes to this command
                    player = Players.add(session, bb.getInt());
                    player.sendAccountData();
                    break;
                case ClientAPI.TO_HUB:
                    /// Hubs singleton subscribes to this command
                    player.switchHub(bb.get());
                    break;
                case ClientAPI.QUIT:
                    // room subscribes to this command
                    break;
                case ClientAPI.TURN_DATA:
                    // room subscribes to this command
                    player.room.onData(player, new TurnData(
                            bb.get(),
                            bb.getFloat(),
                            bb.getFloat(),
                            bb.get(),
                            bb.get()
                    ));
                    break;
                case ClientAPI.END_TURN:
                    // room subscribes to this command
                    player.room.onSync(player, new SyncData(bb.get()));
                    break;
                default:
                    throw new Exception("invalid data received");
            }
        } catch (Throwable e) {
            System.err.println(e.getMessage());
            e.printStackTrace();
            if (player != null) player.disconnect();
            else session.close();
        }
    }
}