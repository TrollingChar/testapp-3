package commands.server;

import annotations.ServerCommandCode;
import players.Player;

import java.nio.ByteBuffer;
import java.util.Collection;
import java.util.Queue;

/**
 * @author trollingchar
 */
@ServerCommandCode(ServerCommandCodes.NEW_GAME)
public class NewGameCmd implements ServerCommand {
    private final int seed;
    private final Collection<Player> players;

    public NewGameCmd(int seed, Collection<Player> players) {
        this.seed = seed;
        this.players = players;
    }

    @Override
    public void serialize(ByteBuffer byteBuffer) {
        byteBuffer.putInt(seed);
        byteBuffer.put((byte) players.size());
        for (Player player : players) byteBuffer.putInt(player.getId());
    }
}
