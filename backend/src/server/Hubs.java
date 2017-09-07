package server;

import server.players.PlayerImpl;

/**
 * Доступ к хабам, где сидят игроки.
 */
public class Hubs {
    static Hub[] hubs;

    public static void init() {
        System.out.println("Hubs.init");
        hubs = new Hub[256];
        for (int i = 1; i < 256; i++) hubs[i] = new Hub(i);
    }

    public static void add(PlayerImpl player, byte hubId) {
        System.out.println("Hubs.add");
        hubs[hubId].add(player);
    }
}
