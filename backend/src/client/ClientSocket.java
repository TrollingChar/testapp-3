package client;

import org.eclipse.jetty.websocket.api.Session;
import org.eclipse.jetty.websocket.api.WebSocketAdapter;
import util.GameLogger;

import java.nio.ByteBuffer;

public class ClientSocket extends WebSocketAdapter {
    public static boolean alive = true;

    @Override
    public void onWebSocketConnect(Session session) {
        super.onWebSocketConnect(session);
        System.out.println("connection: " + session);
    }

    @Override
    public void onWebSocketText(String message) {
        super.onWebSocketText(message);
        System.out.println("Server sent a string: " + message);
        getSession().close();
    }

    @Override
    public void onWebSocketClose(int statusCode, String reason) {
        super.onWebSocketClose(statusCode, reason);
        System.out.println("disconnection: " + statusCode + " " + reason);
        alive = false;
    }

    @Override
    public void onWebSocketError(Throwable cause) {
        super.onWebSocketError(cause);
        cause.printStackTrace(System.err);
    }

    @Override
    public void onWebSocketBinary(byte[] payload, int offset, int len) {
        super.onWebSocketBinary(payload, offset, len);
        GameLogger.println(ByteBuffer.wrap(payload, offset, len));
    }
}
