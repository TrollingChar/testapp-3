using System;
using System.Collections;


namespace Utils {

    public class BitMatrix {

        public int Width { get; private set; }
        public int Height { get; private set; }
        private BitArray _bits;


        public BitMatrix (int w, int h) {
            Width = w;
            Height = h;
            _bits = new BitArray(w * h);
        }


        public BitMatrix (byte[,] array) {
            Width = array.GetLength(0);
            Height = array.GetLength(1);
            _bits = new BitArray(Width * Height);
            
            for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++) {
                this[x, y] = array[x, y] != 0;
            }
        }


        public bool this [int x, int y] {
            // unsafe of course
            get { return _bits[x + y * Width]; }
            set { _bits[x + y * Width] = value; }
        }

    }

}
