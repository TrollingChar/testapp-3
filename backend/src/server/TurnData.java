package server;

/**
 * Created by Дима on 17.04.2017.
 */
public class TurnData {
    public byte flags;
    public byte weapon;
    public byte number;
    public float x, y;

    public TurnData(byte b, float x, float y) {
        flags = b;
        this.x = x;
        this.y = y;
    }
}
