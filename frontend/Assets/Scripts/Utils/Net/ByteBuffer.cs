using System;
using System.Linq;
using Utils.Net.Conversion;


namespace Utils.Net {

    internal class ByteBuffer {

        private static EndianBitConverter _bitConverter = new BigEndianBitConverter();
        public byte[] Bytes;
        public int Length;
        private int _capacity;
        private int _offset;


        public ByteBuffer (int capacity = 64) {
            _capacity = capacity;
            Bytes = new byte[capacity];
            Length = _offset = 0;
        }


        public ByteBuffer (byte[] data) {
            _capacity = Length = Bytes.Length;
            Bytes = data;
            _offset = 0;
        }


        private void Expand (int capacity) {
            if (_capacity >= capacity) return;
            var temp = new byte[capacity];
            _capacity = capacity;
            Bytes.CopyTo(temp, 0);
            Bytes = temp;
        }


        public void WriteByte (byte data) {
            if (Length == _capacity) Expand(_capacity * 2);
            Bytes[Length++] = data;
        }


        public void WriteBytes (byte[] data) {
            int capacity = _capacity;
            while (Length + data.Length > capacity) capacity *= 2;
            Expand(capacity);
            data.CopyTo(Bytes, Length);
            Length += data.Length;
        }


        public void WriteInt16 (Int16 data) {
            WriteBytes(_bitConverter.GetBytes(data));
        }


        public void WriteInt32 (Int32 data) {
            WriteBytes(_bitConverter.GetBytes(data));
        }


        public void WriteInt64 (Int64 data) {
            WriteBytes(_bitConverter.GetBytes(data));
        }


        public void WriteFloat (float data) {
            WriteBytes(_bitConverter.GetBytes(data));
        }


        public void WriteDouble (double data) {
            WriteBytes(_bitConverter.GetBytes(data));
        }


        public void Clear () {
            Length = _offset = 0;
        }


        public static implicit operator byte[] (ByteBuffer bb) {
            return bb.Bytes
                .Skip(bb._offset)
                .Take(bb.Length)
                .ToArray();
        }

    }

}
