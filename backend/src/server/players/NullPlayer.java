package server.players;

import server.command.server.ServerCommand;

public class NullPlayer implements Player {

    @Override
    public void send(ServerCommand cmd) {
    }

    @Override
    public void disconnect() {
    }

    @Override
    public int getId() {
        return -1;
    }
}
