package dto;

public enum DTOs {
    // client commands: ...0
    AUTH_REQUEST(0),
    JOIN_LOBBY(10),
    LEAVE_LOBBY(20),
    TURN_DATA_CLIENT(30),
    TURN_ENDED(40),
    LEAVE_GAME(50),

    // server commands: ...1
    AUTH_SUCCESS(1),
    LOBBY_STATUS(11),
    LEFT_LOBBY(21),
    NEW_GAME(31),
    TURN_DATA_SERVER(41),
    NEW_TURN(51),
    LEFT_GAME(61),
    GAME_ENDED(71),

    // non-command dto: ...2
    ACCOUNT_DATA(2),
    TURN_DATA(12),
    END_TURN_DATA(22),
    END_GAME_DATA(32),
    GAME_INIT_DATA(32),

    ;

    public final short code;


    DTOs (int code) {
        this.code = (short) code;
    }
}
