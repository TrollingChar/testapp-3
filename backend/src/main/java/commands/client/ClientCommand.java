package commands.client;

import players.Player;

import java.nio.ByteBuffer;

public abstract class ClientCommand {
    public Player player;

    public void deserialize(ByteBuffer byteBuffer) {
    }

    public abstract void execute();
}
