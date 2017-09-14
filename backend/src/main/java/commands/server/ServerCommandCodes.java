package commands.server;

public class ServerCommandCodes {

    public static final byte
            AUTHORIZED = 0,
            LOBBY_UPDATED = 1,
            REMOVED_FROM_LOBBY = 2,
            LEFT_GAME = 3,
            GAME_ENDED = 4,
            UPDATE_WORLD = 5,
//            NO_WINNER = 6,
            NEW_TURN = 7,
            NEW_GAME = 8;

    private ServerCommandCodes() {
        // no instance
    }
}
