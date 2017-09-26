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
            new Server(8080).run();
        }
        catch (Exception e) {
            System.err.println("!! FATAL !!");;
            e.printStackTrace();
        }
    }
}
