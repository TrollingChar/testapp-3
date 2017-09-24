package rooms;

import java.util.ArrayList;
import java.util.List;


public class Lobbies {

    private static final List<Lobby> lobbies = new ArrayList<>(16);


    public static Lobby get (int index) {
        return lobbies.get(index);
    }
}
