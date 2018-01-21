using Utils;
using Utils.Random;


namespace Battle.Terrain.Generation {

    public class LandGen {

        //public byte[,] Array;
        public BitMatrix Array;

        // water at Y=0
        // sky at max Y
        // left at X=0
        // right at max X


        public LandGen (byte[,] array) {
            Array = new BitMatrix(array);
        }


        public LandGen (BitMatrix array) {
            Array = array;
        }


        public virtual LandGen Expand (int iterations = 1) {
            BitMatrix
                array = Array,
                result = array;

            for (int i = 0; i < iterations; ++i) {
                int w = array.Width;
                int h = array.Height;
                int resultw = w * 2 - 1;
                int resulth = h * 2 - 1;

                result = new BitMatrix(w * 2 - 1, h * 2 - 1);

                // X-X
                // ---
                // X-X
                for (int x = 0; x < w; ++x)
                for (int y = 0; y < h; ++y) {
                    result[x * 2, y * 2] = array[x, y];
                }

                // #-#
                // -X-
                // #-#
                for (int x = 1; x < resultw; x += 2)
                for (int y = 1; y < resulth; y += 2) {
                    byte chance = 0;
                    if (result[x - 1, y - 1]) chance++;
                    if (result[x + 1, y - 1]) chance++;
                    if (result[x + 1, y + 1]) chance++;
                    if (result[x - 1, y + 1]) chance++;
                    result[x, y] = RNG.Bool(chance, 4);
                }

                // |#-  -#|
                // |X#  #X|
                // |#-  -#|
                for (int y = 1; y < resulth; y += 2) {
                    result[0, y] = result[resultw - 1, y] = false;
                }

                for (int x = 1; x < resultw; x += 2) {
                    // -#-
                    // #X#
                    // ~~~
                    byte chance = 1;
                    if (result[x - 1, 0]) chance++;
                    if (result[x + 1, 0]) chance++;
                    if (result[x,     1]) chance++;
                    result[x, 0] = RNG.Bool(chance, 4);
                    // '''
                    // #X#
                    // -#-
                    result[x, resulth - 1] = false;
                }

                // -#-
                // #X#
                // -#-
                for (int x = 1; x < resultw - 1; ++x)
                for (int y = 1 + (x & 1); y < resulth - 1; y += 2) {
                    byte chance = 0;
                    if (result[x - 1, y]) chance++;
                    if (result[x + 1, y]) chance++;
                    if (result[x, y + 1]) chance++;
                    if (result[x, y + 1]) chance++;
                    result[x, y] = RNG.Bool(chance, 4);
                }
                array = result;
            }
            return new LandGen(result);
        }


        public virtual LandGen Cellular (uint rules, int iterations = 5) {
            uint[] init = {1, 1 << 16};
            BitMatrix
                array = Array,
                result = array;

            int w = array.Width;
            int h = array.Height;

            for (int i = 0; i < iterations; ++i) {
                result = new BitMatrix(w, h);
                uint temp;

                for (int x = 1; x < w - 1; ++x)
                for (int y = 1; y < h - 1; ++y) {
                    temp = init[array[x, y] ? 1 : 0];
                    if(array[x - 1, y - 1]) temp <<= 1;
                    if(array[x, y - 1])     temp <<= 1;
                    if(array[x + 1, y - 1]) temp <<= 1;
                    if(array[x + 1, y])     temp <<= 1;
                    if(array[x + 1, y + 1]) temp <<= 1;
                    if(array[x, y + 1])     temp <<= 1;
                    if(array[x - 1, y + 1]) temp <<= 1;
                    if(array[x - 1, y])     temp <<= 1;
                    result[x, y] = (rules & temp) != 0;
                }

                for (int x = 1; x < w - 1; ++x) {
                    temp = init[array[x, 0] ? 1 : 0] << 2; // water at Y=0
                    if (array[x - 1, 0]) temp <<= 1;
                    if (array[x + 1, 0]) temp <<= 1;
                    if (array[x + 1, 1]) temp <<= 1;
                    if (array[x, 1])     temp <<= 1;
                    if (array[x - 1, 1]) temp <<= 1;
                    result[x, 0] = (rules & temp) != 0;

                    temp = init[array[x, h - 1] ? 1 : 0];
                    if (array[x - 1, h - 1]) temp <<= 1;
                    if (array[x + 1, h - 1]) temp <<= 1;
                    if (array[x + 1, h - 2]) temp <<= 1;
                    if (array[x, h - 2])     temp <<= 1;
                    if (array[x - 1, h - 2]) temp <<= 1;
                    result[x, h - 1] = (rules & temp) != 0;
                }

                for (int y = 1; y < h - 1; ++y) {
                    temp = init[array[0, y] ? 1 : 0];
                    if (array[0, y - 1]) temp <<= 1;
                    if (array[0, y + 1]) temp <<= 1;
                    if (array[1, y + 1]) temp <<= 1;
                    if (array[1, y])     temp <<= 1;
                    if (array[1, y - 1]) temp <<= 1;
                    result[0, y] = (rules & temp) != 0;

                    temp = init[array[w - 1, y] ? 1 : 0];
                    if (array[w - 1, y - 1]) temp <<= 1;
                    if (array[w - 1, y + 1]) temp <<= 1;
                    if (array[w - 2, y + 1]) temp <<= 1;
                    if (array[w - 2, y])     temp <<= 1;
                    if (array[w - 2, y - 1]) temp <<= 1;
                    result[w - 1, y] = (rules & temp) != 0;
                }

                temp = init[array[0, 0] ? 1 : 0];
                if (array[0, 1]) temp <<= 1;
                if (array[1, 1]) temp <<= 1;
                if (array[1, 0]) temp <<= 1;
                result[0, 0] = (rules & temp) != 0;

                temp = init[array[0, h - 1] ? 1 : 0];
                if (array[0, h - 2]) temp <<= 1;
                if (array[1, h - 2]) temp <<= 1;
                if (array[1, h - 1]) temp <<= 1;
                result[0, h - 1] = (rules & temp) != 0;

                temp = init[array[w - 1, h - 1] ? 1 : 0];
                if (array[w - 1, h - 2]) temp <<= 1;
                if (array[w - 2, h - 2]) temp <<= 1;
                if (array[w - 2, h - 1]) temp <<= 1;
                result[w - 1, h - 1] = (rules & temp) != 0;

                temp = init[array[w - 1, 0] ? 1 : 0];
                if (array[w - 1, 1]) temp <<= 1;
                if (array[w - 2, 1]) temp <<= 1;
                if (array[w - 2, 0]) temp <<= 1;
                result[w - 1, 0] = (rules & temp) != 0;
                array = result;
            }
            return new LandGen(result);
        }


        public virtual LandGen Rescale (int w, int h) {
            var result = new BitMatrix(w, h);
            int thisw = Array.Width;
            int thish = Array.Height;

            for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++) {
                result[x, y] = Array[x * thisw / w, y * thish / h];
            }
            return new LandGen(result);
        }


        public virtual LandGen SwitchDimensions () {
            int w = Array.Width,
                h = Array.Height;
            var result = new BitMatrix(h, w);

            for (int x = 0; x < w; ++x) {
                int ix = w - x - 1;
                for (int y = 0; y < h; ++y) result[y, x] = Array[ix, y];
            }
            return new LandGen(result);
        }

    }

}
