package server;

/**
 * Created by Дима on 17.04.2017.
 */
public class SyncData {
    public boolean alive;

    public SyncData(byte alive) {
        this.alive = alive != 0;
    }
}
