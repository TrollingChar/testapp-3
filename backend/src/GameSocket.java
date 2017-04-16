import org.eclipse.jetty.websocket.api.Session;
import org.eclipse.jetty.websocket.api.WebSocketAdapter;

import java.nio.ByteBuffer;

// server side
public class GameSocket extends WebSocketAdapter {

    @Override
    public void onWebSocketConnect(Session session) {
        super.onWebSocketConnect(session);
        System.out.println("client tries to connect");
    }

    @Override
    public void onWebSocketText(String message) {
        super.onWebSocketText(message);
        // text not allowed
        try {
            HQ.disconnect(Lookup.getPlayer(getSession()));
        } catch (Throwable e) {
            getSession().close();
        }
    }

    @Override
    public void onWebSocketClose(int statusCode, String reason) {
        try {
            HQ.disconnect(Lookup.getPlayer(getSession()));
        } catch (Throwable e) {}
        super.onWebSocketClose(statusCode, reason);
        System.out.println("connection closed");
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
        GameLogger.println(bb);

        Session session = getSession();
        Player player = Lookup.getPlayer(session);
        try {
            switch (bb.get()) {
                case ClientAPI.AUTH:
                    player = Lookup.addPlayer(session, bb.getInt());
                    player.sendAccountData();
                    break;
                case ClientAPI.TO_HUB:
                    HQ.movePlayerToHub(player, bb.get());
                    break;
                case ClientAPI.QUIT:
                    player.quitRoom();
                case ClientAPI.TURN_DATA:
                    if(bb.remaining() != 9) throw new Exception();
                    player.doTurn(bb.slice());
                case ClientAPI.END_TURN:
                    // needed to switch active player in room
                    // read hash and number of sub-turns
                    player.endTurn(bb.getInt(), bb.getInt());
                default:
                    // invalid command code
                    HQ.disconnect(player);
                    break;
            }
        } catch (Throwable e) {
            System.err.println("ERROR:" + e.getMessage());
            e.printStackTrace();
            if(player != null) HQ.disconnect(player);
            else session.close();
        }
    }
}
