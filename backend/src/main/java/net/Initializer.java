package net;

import io.netty.channel.ChannelInitializer;
import io.netty.channel.socket.SocketChannel;
import io.netty.handler.codec.http.HttpObjectAggregator;
import io.netty.handler.codec.http.HttpServerCodec;
import io.netty.handler.codec.http.websocketx.WebSocketServerProtocolHandler;


class Initializer extends ChannelInitializer<SocketChannel> {

    @Override
    public void initChannel (SocketChannel ch) throws Exception {
        ch.pipeline().addLast(
            new HttpServerCodec(),
            new HttpObjectAggregator(65536),
            new WebSocketServerProtocolHandler("/websocket"),
//            new WebSocketServerCompressionHandler(),
            new DTOCodec(),
            new CommandHandler()
//            new Echo()
        );
    }
}