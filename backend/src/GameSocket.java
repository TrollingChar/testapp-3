import org.eclipse.jetty.websocket.api.CloseStatus;
import org.eclipse.jetty.websocket.api.Session;
import org.eclipse.jetty.websocket.api.WebSocketAdapter;

import java.io.IOException;
import java.nio.ByteBuffer;

// server side
public class GameSocket extends WebSocketAdapter {
    @Override
    public void onWebSocketConnect(Session session) {
        super.onWebSocketConnect(session);
        System.out.println("connection: " + session);
    }

    @Override
    public void onWebSocketText(String message) {
        super.onWebSocketText(message);
        // text not allowed
        getSession().close(400, "Bad Request");
    }

    @Override
    public void onWebSocketClose(int statusCode, String reason) {
        HQ.disconnect(getSession());
        super.onWebSocketClose(statusCode, reason);
        System.out.println("disconnection: " + statusCode + " " + reason);
    }

    @Override
    public void onWebSocketError(Throwable cause) {
        super.onWebSocketError(cause);
        cause.printStackTrace(System.err);
    }

    @Override
    public void onWebSocketBinary(byte[] payload, int offset, int len) {
        super.onWebSocketBinary(payload, offset, len);

        ByteBuffer bb = ByteBuffer.wrap(payload, offset, len);
        Session session = getSession();
        try {
            switch (bb.get()) {
                case ClientAPI.AUTH:
                    HQ.authorize(session, bb.getInt());
                    break;
                case ClientAPI.START_GAME:
                    HQ.onStartGame(session);
                    break;
                case ClientAPI.CANCEL:
                    HQ.onCancel(session);
                    break;
                case ClientAPI.CHEAT:
                    HQ.onCheat(session);
                    break;
                case ClientAPI.QUIT_GAME:
                    HQ.onQuitGame(session);
                    break;
                case ClientAPI.SYNC:
                    HQ.onSync(session);
                    break;
                case ClientAPI.TURN_DATA:
                    HQ.onTurnData(session);
                    break;
                default:
                    HQ.disconnect(session);
                    break;
            }
        } catch (Exception e) {
            HQ.disconnect(session);
        }

        /*try {
            getRemote().sendBytes(bb);
        } catch (IOException e) {
            e.printStackTrace();
            getSession().close();
        }*/
    }
}
