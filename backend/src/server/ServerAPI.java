package server;

/**
 * Команды, которые сервер отправляет клиенту.
 */
public class ServerAPI {
    public static final byte ACCOUNT_DATA = 0;
    public static final byte HUB_STATUS = 1;
    public static final byte START_GAME = 2;
    public static final byte LEFT_GAME = 3;
    public static final byte WINNER = 4;
    public static final byte TURN_DATA = 5;
    public static final byte NO_WINNER = 6;
    public static final byte NEW_TURN = 7;
}
