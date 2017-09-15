package rooms;

public class Lobbies {
    private static Lobby[] lobbies;


    public static void init () {
        lobbies = new Lobby[256];
        for (int i = 1; i < 256; i++) lobbies[i] = new Lobby(i);
    }


    public static Lobby getLobby (int id) {
        return lobbies[id];
    }
}
