package server.event.client;

public enum ClientEvents {
    Authorize(0),
    JoinLobby(1),
    QuitGame(2),
    TurnData(3),
    EndTurn(4);

    byte code;

    ClientEvents(int code) {
        this.code = (byte) code;
    }
}
