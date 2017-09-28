package net;

import dto.DTO;
import dto.client.AuthRequestCmd;
import dto.client.ClientCommand;
import dto.server.AuthSuccessCmd;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.SimpleChannelInboundHandler;
import players.Player;
import players.Players;


public class CommandHandler extends SimpleChannelInboundHandler<DTO> {

    Player player;


    @Override
    public void handlerAdded (ChannelHandlerContext ctx) throws Exception {
        System.out.println(this + "added");
    }


    protected void channelRead0 (ChannelHandlerContext ctx, DTO dto) throws Exception {
        if (!(dto instanceof ClientCommand)) throw new Exception("This is not a command!");

        ClientCommand cmd = (ClientCommand) dto;
        cmd.player = player;
        synchronized (CommandHandler.class) {
            cmd.execute();

            if (cmd instanceof AuthRequestCmd) {
                player = Players.register(ctx, ((AuthRequestCmd) cmd).id);
                if (player != null) player.send(new AuthSuccessCmd(player.id));
            }
        }
    }
}
