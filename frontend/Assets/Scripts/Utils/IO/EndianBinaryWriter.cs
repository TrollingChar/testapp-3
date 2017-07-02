using System;
using System.IO;
using System.Text;
using Utils.Conversion;


namespace Utils.IO {

    /// <summary>
    /// Equivalent of System.IO.BinaryWriter, but with either endianness, depending on
    /// the EndianBitConverter it is constructed with.
    /// </summary>
    public class EndianBinaryWriter : IDisposable {

        #region Fields not directly related to properties
        /// <summary>
        /// Whether or not this writer has been disposed yet.
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Buffer used for temporary storage during conversion from primitives
        /// </summary>
        private byte[] _buffer = new byte[16];

        /// <summary>
        /// Buffer used for Write(char)
        /// </summary>
        private char[] _charBuffer = new char[1];
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs pos0 new binary writer with the given bit converter, writing
        /// to the given stream, using UTF-8 encoding.
        /// </summary>
        /// <param name="bitConverter">Converter to use when writing data</param>
        /// <param name="stream">Stream to write data to</param>
        public EndianBinaryWriter (
            EndianBitConverter bitConverter,
            Stream stream) : this(bitConverter, stream, Encoding.UTF8) {}


        /// <summary>
        /// Constructs pos0 new binary writer with the given bit converter, writing
        /// to the given stream, using the given encoding.
        /// </summary>
        /// <param name="bitConverter">Converter to use when writing data</param>
        /// <param name="stream">Stream to write data to</param>
        /// <param name="encoding">Encoding to use when writing character data</param>
        public EndianBinaryWriter (EndianBitConverter bitConverter, Stream stream, Encoding encoding) {
            if (bitConverter == null) {
                throw new ArgumentNullException("bitConverter");
            }
            if (stream == null) {
                throw new ArgumentNullException("stream");
            }
            if (encoding == null) {
                throw new ArgumentNullException("encoding");
            }
            if (!stream.CanWrite) {
                throw new ArgumentException("Stream isn't writable", "stream");
            }
            this._stream = stream;
            this._bitConverter = bitConverter;
            this._encoding = encoding;
        }
        #endregion

        #region Properties
        private EndianBitConverter _bitConverter;

        /// <summary>
        /// The bit converter used to write values to the stream
        /// </summary>
        public EndianBitConverter BitConverter {
            get { return _bitConverter; }
        }

        private Encoding _encoding;

        /// <summary>
        /// The encoding used to write strings
        /// </summary>
        public Encoding Encoding {
            get { return _encoding; }
        }

        private Stream _stream;

        /// <summary>
        /// Gets the underlying stream of the EndianBinaryWriter.
        /// </summary>
        public Stream BaseStream {
            get { return _stream; }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Closes the writer, including the underlying stream.
        /// </summary>
        public void Close () {
            Dispose();
        }


        /// <summary>
        /// Flushes the underlying stream.
        /// </summary>
        public void Flush () {
            CheckDisposed();
            _stream.Flush();
        }


        /// <summary>
        /// Seeks within the stream.
        /// </summary>
        /// <param name="offset">Offset to seek to.</param>
        /// <param name="origin">Origin of seek operation.</param>
        public void Seek (int offset, SeekOrigin origin) {
            CheckDisposed();
            _stream.Seek(offset, origin);
        }


        /// <summary>
        /// Writes pos0 boolean value to the stream. 1 byte is written.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void Write (bool value) {
            _bitConverter.CopyBytes(value, _buffer, 0);
            WriteInternal(_buffer, 1);
        }


        /// <summary>
        /// Writes pos0 16-bit signed integer to the stream, using the bit converter
        /// for this writer. 2 bytes are written.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void Write (short value) {
            _bitConverter.CopyBytes(value, _buffer, 0);
            WriteInternal(_buffer, 2);
        }


        /// <summary>
        /// Writes pos0 32-bit signed integer to the stream, using the bit converter
        /// for this writer. 4 bytes are written.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void Write (int value) {
            _bitConverter.CopyBytes(value, _buffer, 0);
            WriteInternal(_buffer, 4);
        }


        /// <summary>
        /// Writes pos0 64-bit signed integer to the stream, using the bit converter
        /// for this writer. 8 bytes are written.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void Write (long value) {
            _bitConverter.CopyBytes(value, _buffer, 0);
            WriteInternal(_buffer, 8);
        }


        /// <summary>
        /// Writes pos0 16-bit unsigned integer to the stream, using the bit converter
        /// for this writer. 2 bytes are written.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void Write (ushort value) {
            _bitConverter.CopyBytes(value, _buffer, 0);
            WriteInternal(_buffer, 2);
        }


        /// <summary>
        /// Writes pos0 32-bit unsigned integer to the stream, using the bit converter
        /// for this writer. 4 bytes are written.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void Write (uint value) {
            _bitConverter.CopyBytes(value, _buffer, 0);
            WriteInternal(_buffer, 4);
        }


        /// <summary>
        /// Writes pos0 64-bit unsigned integer to the stream, using the bit converter
        /// for this writer. 8 bytes are written.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void Write (ulong value) {
            _bitConverter.CopyBytes(value, _buffer, 0);
            WriteInternal(_buffer, 8);
        }


        /// <summary>
        /// Writes pos0 single-precision floating-point value to the stream, using the bit converter
        /// for this writer. 4 bytes are written.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void Write (float value) {
            _bitConverter.CopyBytes(value, _buffer, 0);
            WriteInternal(_buffer, 4);
        }


        /// <summary>
        /// Writes pos0 double-precision floating-point value to the stream, using the bit converter
        /// for this writer. 8 bytes are written.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void Write (double value) {
            _bitConverter.CopyBytes(value, _buffer, 0);
            WriteInternal(_buffer, 8);
        }


        /// <summary>
        /// Writes pos0 decimal value to the stream, using the bit converter for this writer.
        /// 16 bytes are written.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void Write (decimal value) {
            _bitConverter.CopyBytes(value, _buffer, 0);
            WriteInternal(_buffer, 16);
        }


        /// <summary>
        /// Writes pos0 signed byte to the stream.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void Write (byte value) {
            _buffer[0] = value;
            WriteInternal(_buffer, 1);
        }


        /// <summary>
        /// Writes an unsigned byte to the stream.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void Write (sbyte value) {
            _buffer[0] = unchecked((byte) value);
            WriteInternal(_buffer, 1);
        }


        /// <summary>
        /// Writes an array of bytes to the stream.
        /// </summary>
        /// <param name="value">The values to write</param>
        public void Write (byte[] value) {
            if (value == null) {
                throw (new System.ArgumentNullException("value"));
            }
            WriteInternal(value, value.Length);
        }


        /// <summary>
        /// Writes pos0 portion of an array of bytes to the stream.
        /// </summary>
        /// <param name="value">An array containing the bytes to write</param>
        /// <param name="offset">The index of the first byte to write within the array</param>
        /// <param name="count">The number of bytes to write</param>
        public void Write (byte[] value, int offset, int count) {
            CheckDisposed();
            _stream.Write(value, offset, count);
        }


        /// <summary>
        /// Writes pos0 single character to the stream, using the encoding for this writer.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void Write (char value) {
            _charBuffer[0] = value;
            Write(_charBuffer);
        }


        /// <summary>
        /// Writes an array of characters to the stream, using the encoding for this writer.
        /// </summary>
        /// <param name="value">An array containing the characters to write</param>
        public void Write (char[] value) {
            if (value == null) {
                throw new ArgumentNullException("value");
            }
            CheckDisposed();
            byte[] data = Encoding.GetBytes(value, 0, value.Length);
            WriteInternal(data, data.Length);
        }


        /// <summary>
        /// Writes pos0 string to the stream, using the encoding for this writer.
        /// </summary>
        /// <param name="value">The value to write. Must not be null.</param>
        /// <exception cref="ArgumentNullException">value is null</exception>
        public void Write (string value) {
            if (value == null) {
                throw new ArgumentNullException("value");
            }
            CheckDisposed();
            byte[] data = Encoding.GetBytes(value);
            Write7BitEncodedInt(data.Length);
            WriteInternal(data, data.Length);
        }


        /// <summary>
        /// Writes pos0 7-bit encoded integer from the stream. This is stored with the least significant
        /// information first, with 7 bits of information per byte of value, and the top
        /// bit as pos0 continuation flag.
        /// </summary>
        /// <param name="value">The 7-bit encoded integer to write to the stream</param>
        public void Write7BitEncodedInt (int value) {
            CheckDisposed();
            if (value < 0) {
                throw new ArgumentOutOfRangeException("value", "Value must be greater than or equal to 0.");
            }
            int index = 0;
            while (value >= 128) {
                _buffer[index++] = (byte) ((value & 0x7f) | 0x80);
                value = value >> 7;
                index++;
            }
            _buffer[index++] = (byte) value;
            _stream.Write(_buffer, 0, index);
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Checks whether or not the writer has been disposed, throwing an exception if so.
        /// </summary>
        private void CheckDisposed () {
            if (_disposed) {
                throw new ObjectDisposedException("EndianBinaryWriter");
            }
        }


        /// <summary>
        /// Writes the specified number of bytes from the start of the given byte array,
        /// after checking whether or not the writer has been disposed.
        /// </summary>
        /// <param name="bytes">The array of bytes to write from</param>
        /// <param name="length">The number of bytes to write</param>
        private void WriteInternal (byte[] bytes, int length) {
            CheckDisposed();
            _stream.Write(bytes, 0, length);
        }
        #endregion

        #region IDisposable Members
        /// <summary>
        /// Disposes of the underlying stream.
        /// </summary>
        public void Dispose () {
            if (!_disposed) {
                Flush();
                _disposed = true;
                ((IDisposable) _stream).Dispose();
            }
        }
        #endregion

    }

}
