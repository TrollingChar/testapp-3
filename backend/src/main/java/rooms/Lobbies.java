package rooms;

import java.util.ArrayList;
import java.util.List;


public class Lobbies {

    private static final List<Lobby> lobbies = new ArrayList<>();


    public static void init () {
        lobbies.add(null);
        for (int i = 1; i < 16; ++i) lobbies.add(new Lobby(i));
    }


    public static Lobby get (int index) {
        return lobbies.get(index);
    }
}
