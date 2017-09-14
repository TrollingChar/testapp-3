package commands.client;

import annotations.ClientCommandCode;
import rooms.Lobby;

@ClientCommandCode(ClientCommandCodes.REMOVE_FROM_LOBBY)
public class RemoveFromLobbyCmd extends ClientCommand {

    @Override
    public void execute() {
        Lobby lobby = (Lobby) player.getRoom(room -> room instanceof Lobby);
        if (lobby != null) player.quitRoom(lobby);
    }
}
