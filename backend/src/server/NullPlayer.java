package server;

import org.eclipse.jetty.websocket.api.Session;

/**
 * Created by Дима on 27.06.2017.
 */

// will se sending nothing in SEND methods
public class NullPlayer extends Player {
    public NullPlayer(Session session, int id) {
        super(session, id);
    }
}
