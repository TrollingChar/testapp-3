package commands.client;

import annotations.ClientCommandCode;
import commands.server.AuthorizedCmd;
import org.eclipse.jetty.websocket.api.Session;
import players.Players;

import java.nio.ByteBuffer;

@ClientCommandCode(ClientCommandCodes.AUTHORIZE)
public class AuthorizeCmd extends ClientCommand {

    public Session session;
    private int id;

    @Override
    public void deserialize(ByteBuffer byteBuffer) {
        id = byteBuffer.getInt();
    }

    @Override
    public void execute() {
        if (player != null) {
            // todo: bind info to player
            System.err.println("Double authorization attempt!");
            return;
        }
        player = Players.login(session, id);
        player.send(new AuthorizedCmd(id));
    }
}
