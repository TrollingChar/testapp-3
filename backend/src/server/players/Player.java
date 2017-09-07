package server.players;

import server.command.server.ServerCommand;

public interface Player {
    void send(ServerCommand cmd);
    void disconnect();
    int getId();
}
