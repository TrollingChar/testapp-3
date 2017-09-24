package net;

import dto.DTO;
import dto.cmd.client.AuthRequestCmd;
import dto.cmd.client.ClientCommand;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.SimpleChannelInboundHandler;
import players.Players;


public class CommandHandler extends SimpleChannelInboundHandler<DTO> {


    protected void channelRead0 (ChannelHandlerContext ctx, DTO dto) throws Exception {
        if (!(dto instanceof ClientCommand)) throw new Exception("This is not a command!");

        ClientCommand cmd = (ClientCommand) dto;
        cmd.player = Players.get(ctx);
        cmd.execute();
        if (cmd instanceof AuthRequestCmd) {
            Players.register(ctx, ((AuthRequestCmd) cmd).id);
        }
    }
}
