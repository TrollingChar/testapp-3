package server;

import java.io.IOException;
import java.nio.ByteBuffer;
import java.util.*;

/**
 * Здесь проходит сама игра.
 *
 * Возможные читы:
 *   взлом хп на клиенте
 *   взлом оружия на клиенте
 *   ход не в свою очередь
 *   попытка помешать выполнению критерия конца боя
 *   затягивание хода
 * Как им противостоять:
 *   сохранять реплеи (особенно подозрительные)
 *   считать хэш в конце хода и проверять на сервере совпадение
 *   считать длительность ходов
 *   отправлять клиентам данные хода вместе с номером того кто их отправил
 *   в конце хода отправлять список оставшихся в игре игроков
 *   клиенты могут кидать репорт если долго нет данных
 */
public class Room {
    Set<Player> observers; // игроки, которые наблюдают игру или сами играют
    Queue<Player> players; // игроки, которые играют и по очереди делают ходы
    Replay replay;
    Map<Player, SyncData> syncData; // данные синхронизации игры

    public Room(Set<Player> players) {
        System.out.println("Room.Room");
        observers = new HashSet<>(players);
        this.players = new LinkedList<>(players);
        replay = new Replay();
        syncData = new HashMap<>();
    }
/*
    public void add(Player player) {
        System.out.println("Room.add");
        player.room = this;
        players.add(player);
        observers.add(player);
    }

    public void addObserver(Player observer) {
        System.out.println("Room.addObserver");
        observer.room = this;
        observers.add(observer);
    }
*/
    public void startGame() {
        System.out.println("Room.startGame");
        Collections.shuffle((List<?>) players);
        sendStartGame();
    }

    private Collection<Player> sendToAll(ByteBuffer bb) {
        System.out.println("Room.sendToAll");
        Collection<Player> discon = new LinkedList<>();
        for (Iterator<Player> it = observers.iterator(); it.hasNext(); ) {
            Player observer = it.next();
            try {
                observer.send(bb.duplicate());
            } catch (IOException e) {
                observer.room = null;
                observer.disconnect();
                it.remove();
                players.remove(observer);
                discon.add(observer);
            }
        }
        return discon;
    }

    private void sendStartGame() {
        System.out.println("Room.sendStartGame");
        byte[] seed = new byte[4];
        GameServer.random.nextBytes(seed);
        ByteBuffer bb = ByteBuffer
                .allocate(6 + 4 * players.size())
                .put(ServerAPI.START_GAME)
                .put(seed)
                .put((byte) players.size());
        for (Player player : players) bb.putInt(player.id);
        Collection<Player> discon = sendToAll(bb);
        // todo handle disconnected players
    }

    public void onSync(Player player, SyncData data) {
        System.out.println("Room.onSync");
        Replay.log(player, data);
    }

    public void onData(Player player, TurnData data) {
        System.out.println("Room.onData");
        Replay.log(player, data);
    }

    public void onSurrender(Player player) {
        System.out.println("Room.onSurrender");
        Replay.logSurrender(player);
    }

    public void remove(Player player) {
        System.out.println("Room.remove");
        // todo handle disconnections
    }
}
