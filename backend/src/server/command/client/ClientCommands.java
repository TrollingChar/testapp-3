package server.command.client;

public enum ClientCommands {
    Authorize(0),
    EnterHub(1),
    ;

    byte code;

    ClientCommands(int code) {
        this.code = (byte) code;
    }
}
