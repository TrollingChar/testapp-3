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
    Player activePlayer;

    public Room(Set<Player> players) {
        System.out.println("Room.Room");
        for(Player player : players) player.room = this;
        observers = new HashSet<>(players);
        this.players = new LinkedList<>(players);
        replay = new Replay();
        syncData = new HashMap<>();
    }

    public void startGame() {
        System.out.println("Room.startGame");
        Collections.shuffle((List<?>) players);
        Collection<Player> discon = sendStartGame();
        while (discon.size() > 0) discon = sendQuitGame(discon);
        //newTurn();
    }

    private Collection<Player> sendToAll(ByteBuffer bb, Player player) {
        System.out.println("Room.sendToAll");
        Collection<Player> discon = new LinkedList<>();
        for (Iterator<Player> it = observers.iterator(); it.hasNext(); ) {
            Player observer = it.next();
            if (observer == player) continue;
            try {
                observer.send(bb.duplicate());
            } catch (IOException e) {
                observer.room = null;
                observer.disconnect();
                it.remove();
                if (players.remove(observer)) discon.add(observer);
                // so discon will contain only players, not observers
            }
        }
        return discon;
        // we do not handle activePlayer here
    }

    private Collection<Player> sendToAll(ByteBuffer bb) {
        return sendToAll(bb, null);
    }

    private Collection<Player> sendStartGame() {
        System.out.println("Room.sendStartGame");
        byte[] seed = new byte[4];
        GameServer.random.nextBytes(seed);
        ByteBuffer bb = ByteBuffer
                .allocate(6 + 4 * players.size())
                .put(ServerAPI.START_GAME)
                .put(seed)
                .put((byte) players.size());
        for (Player player : players) bb.putInt(player.id);
        return sendToAll(bb);
    }

    private Collection<Player> sendQuitGame(Player player) {
        ByteBuffer bb = ByteBuffer
                .allocate(6)
                .put(ServerAPI.LEFT_GAME)
                .put((byte) 1)
                .putInt(player.id);
        return sendToAll(bb);
    }

    private Collection<Player> sendQuitGame(Collection<Player> discon) {
        ByteBuffer bb = ByteBuffer
                .allocate(2 + discon.size()*4)
                .put(ServerAPI.LEFT_GAME)
                .put((byte) discon.size());
        for (Player player : discon) bb.putInt(player.id);
        return sendToAll(bb);
    }

    public void onSync(Player player, SyncData data) {
        System.out.println("Room.onSync");
        replay.log(player, data);

        if(!players.contains(player)) return;
        if(!data.alive) players.remove(player);
        else syncData.put(player, data);
        if (syncData.size() == players.size()) newTurn();
    }

    public void onData(Player player, TurnData data) {
        System.out.println("Room.onData");
        replay.log(player, data);

        if(player != activePlayer) return;
        ByteBuffer bb = ByteBuffer
                .allocate(10)
                .put(ServerAPI.TURN_DATA)
                .put(data.flags)
                .putFloat(data.x)
                .putFloat(data.y);
        Collection<Player> discon = sendToAll(bb, player);
        while (discon.size() > 0) sendQuitGame(discon);
        if (!players.contains(activePlayer)) newTurn();
    }

    public void remove(Player player) {
        System.out.println("Room.remove");
        Replay.logSurrender(player);
        player.room = null;
        observers.remove(player);
        players.remove(player);
        Collection<Player> discon = sendQuitGame(player);
        while (discon.size() > 0) sendQuitGame(discon);
        if (!players.contains(activePlayer)) newTurn();
    }

    private void newTurn() {
        syncData.clear();
        do {
            // is there a winner?
            if (players.size() < 2) {
                if (players.size() == 1) sendWin(players.remove());
                else sendDraw();
                endGame();
                return;
            }
            players.add(activePlayer = players.remove());
            Collection<Player> discon = sendNewTurn();
            while (discon.size() > 0) sendQuitGame(discon);
        } while (!players.contains(activePlayer));
    }

    private void endGame() {
        for (Player observer : observers) observer.room = null;
        observers.clear();
        players.clear();
        syncData.clear();
        activePlayer = null;
        replay.save();
    }

    private Collection<Player> sendNewTurn() {
        ByteBuffer bb = ByteBuffer
                .allocate(5)
                .put(ServerAPI.NEW_TURN)
                .putInt(activePlayer.id);
        return sendToAll(bb);
    }

    private void sendDraw() {
        ByteBuffer bb = ByteBuffer
                .allocate(1)
                .put(ServerAPI.NO_WINNER);
        sendToAll(bb);
    }

    private void sendWin(Player winner) {
        ByteBuffer bb = ByteBuffer
                .allocate(5)
                .put(ServerAPI.WINNER)
                .putInt(winner.id);
        sendToAll(bb);
    }

}
