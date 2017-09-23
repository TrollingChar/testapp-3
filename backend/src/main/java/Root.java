import dto.DTO;
import net.Server;


public class Root {

    public static void main (String[] args) {
        try {
            DTO.init();
            new Server(8080).run();
        }
        catch (Exception e) {
            System.err.println("!! FATAL !!");;
            e.printStackTrace();
        }
    }
}
