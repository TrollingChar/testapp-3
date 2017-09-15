package commands.client;

public class ClientCommandCodes {

    public static final byte
        AUTHORIZE = 0,
        JOIN_LOBBY = 1,
        LEAVE_GAME = 2,
        TURN_DATA = 3,
        END_TURN = 4,
        REMOVE_FROM_LOBBY = 5;


    private ClientCommandCodes () {
        // no instance
    }
}
