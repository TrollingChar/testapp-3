package net;

import dto.DTO;
import io.netty.buffer.ByteBuf;
import io.netty.buffer.PooledByteBufAllocator;
import io.netty.channel.ChannelHandlerContext;
import io.netty.handler.codec.MessageToMessageCodec;
import io.netty.handler.codec.http.websocketx.BinaryWebSocketFrame;

import java.util.List;


class DTOCodec extends MessageToMessageCodec<BinaryWebSocketFrame, DTO> {
    // https://netty.io/4.1/api/io/netty/handler/codec/MessageToMessageCodec.html


    protected void encode (ChannelHandlerContext ctx, DTO dto, List<Object> list) throws Exception {
        System.out.println("sendind " + dto);
        ByteBuf buffer = PooledByteBufAllocator.DEFAULT.buffer();
        dto.write(buffer);
        list.add(new BinaryWebSocketFrame(buffer));
    }


    protected void decode (ChannelHandlerContext ctx, BinaryWebSocketFrame frame, List<Object> list) throws Exception {
        DTO dto = DTO.read(frame.content());
        System.out.println("received " + dto);
        list.add(dto);
    }
}
