package core;

import dto.DTO;
import net.Server;
import rooms.Lobbies;

import java.util.Random;


public class Root {
    public static final Random random = new Random();


    public static void main (String[] args) {
        try {
            DTO.init();
            Lobbies.init();
            String portStr = System.getenv("PORT");
            System.out.println("$PORT: " + portStr);
            int port = portStr == null ? 7675 : Integer.valueOf(portStr);
            new Server(port).run();
        }
        catch (Exception e) {
            System.err.println("!! FATAL !!");
            e.printStackTrace();
        }
    }
}
