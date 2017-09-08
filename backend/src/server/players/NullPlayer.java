package server.players;

import server.event.server.ServerEvent;

public class NullPlayer implements Player {

    @Override
    public void send(ServerEvent cmd) {
    }

    @Override
    public void disconnect() {
    }

    @Override
    public int getId() {
        return -1;
    }
}
