package commands.server;

import annotations.ServerCommandCode;
import players.Player;

/**
 * @author trollingchar
 */
@ServerCommandCode(ServerCommandCodes.LEFT_GAME)
public class LeftGameCmd implements ServerCommand {
    private int player;

    public LeftGameCmd(int player) {
        this.player = player;
    }
}
