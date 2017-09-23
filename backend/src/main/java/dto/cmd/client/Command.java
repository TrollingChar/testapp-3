package dto.cmd.client;

import dto.DTO;
import players.Player;


public abstract class Command extends DTO {

    public Player player;


    public abstract void execute ();
}
