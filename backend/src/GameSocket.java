import org.eclipse.jetty.websocket.api.CloseStatus;
import org.eclipse.jetty.websocket.api.Session;
import org.eclipse.jetty.websocket.api.WebSocketAdapter;

import java.io.IOException;
import java.nio.ByteBuffer;

// server side
public class GameSocket extends WebSocketAdapter {
    @Override
    public void onWebSocketConnect(Session sess) {
        super.onWebSocketConnect(sess);
        System.out.println("connection: " + sess);
    }

    @Override
    public void onWebSocketText(String message) {
        super.onWebSocketText(message);
        // text not allowed
        getSession().close(400, "Bad Request");
    }

    @Override
    public void onWebSocketClose(int statusCode, String reason) {
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
        Session sess = getSession();

        switch (bb.get()) {
            case ClientAPI.AUTH:
                HQ.authorize(sess, bb.getInt());
                break;
            case ClientAPI.START_GAME:
                HQ.onStartGame(sess);
                break;
            case ClientAPI.CANCEL:
                HQ.onCancel(sess);
                break;
            case ClientAPI.CHEAT:
                HQ.onCheat(sess);
                break;
            case ClientAPI.QUIT_GAME:
                HQ.onQuitGame(sess);
                break;
            case ClientAPI.SYNC:
                HQ.onSync(sess);
                break;
            case ClientAPI.TURN_DATA:
                HQ.onTurnData(sess);
                break;
            default:
                HQ.disconnect(sess);
                break;
        }

        /*try {
            getRemote().sendBytes(bb);
        } catch (IOException e) {
            e.printStackTrace();
            getSession().close();
        }*/
    }
}
