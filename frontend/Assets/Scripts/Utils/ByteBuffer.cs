using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiscUtil.Conversion;

class ByteBuffer {
    static EndianBitConverter bitConverter = new BigEndianBitConverter();
    public byte[] bytes;
    public int length;
    int capacity;
    int offset;

    public ByteBuffer (int capacity = 64) {
        this.capacity = capacity;
        bytes = new byte[capacity];
        length = offset = 0;
    }

    public ByteBuffer (byte[] data) {
        capacity = length = bytes.Length;
        bytes = data;
        offset = 0;
    }

    private void Expand (int capacity) {
        if (this.capacity >= capacity) return;
        byte[] temp = new byte[capacity];
        this.capacity = capacity;
        bytes.CopyTo(temp, 0);
        bytes = temp;
    }

    public void WriteByte (byte data) {
        if (length == capacity) Expand(capacity * 2);
        bytes[length++] = data;
    }

    public void WriteBytes (byte[] data) {
        int capacity = this.capacity;
        while (length + data.Length > capacity) capacity *= 2;
        Expand(capacity);
        data.CopyTo(bytes, length);
        length += data.Length;
    }

    public void WriteInt16   (Int16 data) { WriteBytes(bitConverter.GetBytes(data)); }
    public void WriteInt32   (Int32 data) { WriteBytes(bitConverter.GetBytes(data)); }
    public void WriteInt64   (Int64 data) { WriteBytes(bitConverter.GetBytes(data)); }
    public void WriteFloat   (float data) { WriteBytes(bitConverter.GetBytes(data)); }
    public void WriteDouble (double data) { WriteBytes(bitConverter.GetBytes(data)); }

    public void Clear () {
        length = offset = 0;
    }

    public static implicit operator byte[] (ByteBuffer bb) {
        return bb.bytes
            .Skip(bb.offset)
            .Take<byte>(bb.length)
            .ToArray<byte>();
    }
}

