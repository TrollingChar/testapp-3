package net;

import commands.Commands;
import commands.client.AuthorizeCmd;
import commands.client.ClientCommand;
import org.eclipse.jetty.websocket.api.Session;
import org.eclipse.jetty.websocket.api.WebSocketAdapter;
import players.Player;
import util.ByteBufferLogger;

import java.nio.ByteBuffer;


public class GameServerSocket extends WebSocketAdapter {
    private Player player;


    @Override
    public void onWebSocketConnect (Session sess) {
        super.onWebSocketConnect(sess);
    }


    @Override
    public void onWebSocketBinary (byte[] payload, int offset, int len) {
        // empty array is ping
        if (len == 0) return;

        ByteBuffer bb = ByteBuffer.wrap(payload, offset, len);
        ByteBufferLogger.log(bb);
        byte code = bb.get();

        // resolve command
        ClientCommand cmd = Commands.resolveClientCommand(code);
        cmd.deserialize(bb);
        cmd.player = player;
        if (cmd instanceof AuthorizeCmd) ((AuthorizeCmd) cmd).session = getSession();

        // execute command
        cmd.execute();
        player = cmd.player;
    }


    @Override
    public void onWebSocketText (String message) {
        // no text allowed
    }


    @Override
    public void onWebSocketError (Throwable cause) {
        System.err.println("WEBSOCKET ERROR");
        cause.printStackTrace();
    }


    @Override
    public void onWebSocketClose (int statusCode, String reason) {
        if (player != null) {
            player.disconnect();
        }
        super.onWebSocketClose(statusCode, reason);
    }
}
