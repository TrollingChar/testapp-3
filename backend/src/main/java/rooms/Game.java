package rooms;

import core.Root;
import dto.cmd.server.LeftGameCmd;
import dto.cmd.server.NewGameCmd;
import players.Player;

import java.util.Collections;
import java.util.List;


public class Game extends Room {

    public Game (List<Player> players) {
        for (Player player : players) player.joinRoom(this);
        Collections.shuffle(this.players);
        start();
    }


    private void start () {
        broadcast(new NewGameCmd(Root.random.nextInt(), this.players));
    }


    @Override
    protected void onRemovePlayer (Player player) {
        broadcast(new LeftGameCmd(player.id));
    }
}
