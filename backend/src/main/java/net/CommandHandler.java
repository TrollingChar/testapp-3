package net;

import dto.DTO;
import dto.cmd.client.Command;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.SimpleChannelInboundHandler;


public class CommandHandler extends SimpleChannelInboundHandler<DTO> {


    protected void channelRead0 (ChannelHandlerContext ctx, DTO dto) throws Exception {
        if (!(dto instanceof Command)) throw new Exception("This is not a command!");
        Command cmd = (Command) dto;
//        cmd.player = Players.get(ctx);
        cmd.execute();
    }
}
