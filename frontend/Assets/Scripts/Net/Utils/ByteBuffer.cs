using System.Linq;
using Net.Utils.Conversion;


namespace Net.Utils {

    internal class ByteBuffer {

        private static readonly EndianBitConverter _bitConverter = new BigEndianBitConverter();
        private int _capacity;
        private int _offset;
        public byte[] Bytes;
        public int Length;


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


        public void WriteInt16 (short data) {
            WriteBytes(_bitConverter.GetBytes(data));
        }


        public void WriteInt32 (int data) {
            WriteBytes(_bitConverter.GetBytes(data));
        }


        public void WriteInt64 (long data) {
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
