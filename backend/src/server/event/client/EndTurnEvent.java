package server.event.client;

import server.annotations.ClientEventCode;

/**
 * @author trollingchar
 */
@ClientEventCode(ClientEvents.EndTurn)
public class EndTurnEvent extends ClientEvent{
}
