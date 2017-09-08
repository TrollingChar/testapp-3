package server.event;

/**
 * @author trollingchar
 */
public class Dispatcher {

    public static <T> void addListener(java.util.function.Consumer<T> handler) {}
    public static <T> void removeListener(java.util.function.Consumer<T> handler) {}
    public static <T> void dispatch(T msg) {}

}
