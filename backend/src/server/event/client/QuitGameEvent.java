package server.event.client;

import server.annotations.ClientEventCode;

/**
 * @author trollingchar
 */
@ClientEventCode(ClientEvents.QuitGame)
public class QuitGameEvent extends ClientEvent {
}
