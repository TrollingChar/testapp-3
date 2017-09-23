package net;

import io.netty.channel.ChannelHandler;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.SimpleChannelInboundHandler;
import io.netty.handler.codec.http.websocketx.BinaryWebSocketFrame;
import io.netty.handler.codec.http.websocketx.TextWebSocketFrame;
import io.netty.handler.codec.http.websocketx.WebSocketFrame;


public class Echo extends SimpleChannelInboundHandler {

    @Override
    public void handlerAdded (ChannelHandlerContext ctx) throws Exception {
        System.out.println("client connected");
    }


    @Override
    public void handlerRemoved (ChannelHandlerContext ctx) throws Exception {
        System.out.println("client disconnected");
    }


    @Override
    protected void channelRead0 (ChannelHandlerContext ctx, Object o) throws Exception {
        System.out.println(o);
    }
}
