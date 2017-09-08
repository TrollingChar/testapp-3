package server.rooms;

import server.event.Dispatcher;
import server.event.client.EndTurnEvent;
import server.event.client.QuitGameEvent;
import server.event.client.TurnDataEvent;
import server.players.Player;

public class Battle extends Room {

    public Battle () {
        Dispatcher.addListener(this::onQuitGame);
        Dispatcher.addListener(this::onTurnData);
        Dispatcher.addListener(this::onEndTurn);
    }

    private void onEndTurn(EndTurnEvent event) {

    }

    private void onTurnData(TurnDataEvent event) {

    }

    private void onQuitGame(QuitGameEvent event) {

    }

    @Override
    public void addPlayer(Player player) {
        super.addPlayer(player);
    }

    @Override
    public void removePlayer(Player player) {
        super.removePlayer(player);
    }
}
