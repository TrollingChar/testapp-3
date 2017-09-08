package server.players;

import server.event.server.ServerEvent;

public interface Player {
    void send(ServerEvent cmd);
    void disconnect();
    int getId();
}
