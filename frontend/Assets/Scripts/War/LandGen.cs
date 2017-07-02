namespace W3 {

    public class LandGen {

        public byte[,] array;

        // water at Y=0
        // sky at max Y
        // left at X=0
        // right at max X


        public LandGen (byte[,] array) {
            this.array = array;
        }


        public LandGen Expand (int iterations = 1) {
            byte[,]
                array = this.array,
                result = array;

            for (int i = 0; i < iterations; ++i) {
                int w = array.GetLength(0);
                int h = array.GetLength(1);
                int resultw = w * 2 - 1;
                int resulth = h * 2 - 1;
                result = new byte[w * 2 - 1, h * 2 - 1];

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
                    result[x, y] = (byte) (RNG.Bool(
                        result[x - 1, y - 1] +
                        result[x + 1, y - 1] +
                        result[x + 1, y + 1] +
                        result[x - 1, y + 1],
                        4
                    ) ? 1 : 0);
                }

                // |#-  -#|
                // |X#  #X|
                // |#-  -#|
                for (int y = 1; y < resulth; y += 2) {
                    result[0, y] = result[resultw - 1, y] = 0;
                }

                for (int x = 1; x < resultw; x += 2) {
                    // -#-
                    // #X#
                    // ~~~
                    result[x, 0] = (byte) (RNG.Bool(
                        result[x - 1, 0] +
                        result[x + 1, 0] +
                        result[x, 1] + 1,
                        4
                    ) ? 1 : 0);
                    // '''
                    // #X#
                    // -#-
                    result[x, resulth - 1] = 0;
                }

                // -#-
                // #X#
                // -#-
                for (int x = 1; x < resultw - 1; ++x)
                for (int y = (x & 1) != 0 ? 2 : 1; y < resulth - 1; y += 2) {
                    result[x, y] = (byte) (RNG.Bool(
                        result[x - 1, y] +
                        result[x + 1, y] +
                        result[x, y - 1] +
                        result[x, y + 1],
                        4
                    ) ? 1 : 0);
                }
                array = result;
            }
            return new LandGen(result);
        }


        public LandGen Cellular (uint rules, int iterations = 5) {
            uint[] init = {1, 1 << 16};
            byte[,]
                array = this.array,
                result = array;

            int w = array.GetLength(0);
            int h = array.GetLength(1);

            for (int i = 0; i < iterations; ++i) {
                result = new byte[w, h];
                uint temp;

                for (int x = 1; x < w - 1; ++x)
                for (int y = 1; y < h - 1; ++y) {
                    temp = init[array[x, y]];
                    temp <<= array[x - 1, y - 1];
                    temp <<= array[x, y - 1];
                    temp <<= array[x + 1, y - 1];
                    temp <<= array[x + 1, y];
                    temp <<= array[x + 1, y + 1];
                    temp <<= array[x, y + 1];
                    temp <<= array[x - 1, y + 1];
                    temp <<= array[x - 1, y];
                    result[x, y] = (byte) ((rules & temp) != 0 ? 1 : 0);
                }

                for (int x = 1; x < w - 1; ++x) {
                    temp = init[array[x, 0]] << 2; // water at Y=0
                    temp <<= array[x - 1, 0];
                    temp <<= array[x + 1, 0];
                    temp <<= array[x + 1, 1];
                    temp <<= array[x, 1];
                    temp <<= array[x - 1, 1];
                    result[x, 0] = (byte) ((rules & temp) != 0 ? 1 : 0);

                    temp = init[array[x, h - 1]];
                    temp <<= array[x - 1, h - 1];
                    temp <<= array[x + 1, h - 1];
                    temp <<= array[x + 1, h - 2];
                    temp <<= array[x, h - 2];
                    temp <<= array[x - 1, h - 2];
                    result[x, h - 1] = (byte) ((rules & temp) != 0 ? 1 : 0);
                }

                for (int y = 1; y < h - 1; ++y) {
                    temp = init[array[0, y]];
                    temp <<= array[0, y - 1];
                    temp <<= array[0, y + 1];
                    temp <<= array[1, y + 1];
                    temp <<= array[1, y];
                    temp <<= array[1, y - 1];
                    result[0, y] = (byte) ((rules & temp) != 0 ? 1 : 0);

                    temp = init[array[w - 1, y]];
                    temp <<= array[w - 1, y - 1];
                    temp <<= array[w - 1, y + 1];
                    temp <<= array[w - 2, y + 1];
                    temp <<= array[w - 2, y];
                    temp <<= array[w - 2, y - 1];
                    result[w - 1, y] = (byte) ((rules & temp) != 0 ? 1 : 0);
                }

                temp = init[array[0, 0]];
                temp <<= array[0, 1];
                temp <<= array[1, 1];
                temp <<= array[1, 0];
                result[0, 0] = (byte) ((rules & temp) != 0 ? 1 : 0);

                temp = init[array[0, h - 1]];
                temp <<= array[0, h - 2];
                temp <<= array[1, h - 2];
                temp <<= array[1, h - 1];
                result[0, h - 1] = (byte) ((rules & temp) != 0 ? 1 : 0);

                temp = init[array[w - 1, h - 1]];
                temp <<= array[w - 1, h - 2];
                temp <<= array[w - 2, h - 2];
                temp <<= array[w - 2, h - 1];
                result[w - 1, h - 1] = (byte) ((rules & temp) != 0 ? 1 : 0);

                temp = init[array[w - 1, 0]];
                temp <<= array[w - 1, 1];
                temp <<= array[w - 2, 1];
                temp <<= array[w - 2, 0];
                result[w - 1, 0] = (byte) ((rules & temp) != 0 ? 1 : 0);
                array = result;
            }
            return new LandGen(result);
        }


        public LandGen Rescale (int w, int h) {
            var result = new byte[w, h];
            int thisw = array.GetLength(0);
            int thish = array.GetLength(1);

            for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++) {
                result[x, y] = array[x * thisw / w, y * thish / h];
            }
            return new LandGen(result);
        }


        public LandGen SwitchDimensions () {
            int w = array.GetLength(0),
                h = array.GetLength(1);
            var result = new byte[h, w];

            for (int x = 0; x < w; ++x) {
                int ix = w - x - 1;
                for (int y = 0; y < h; ++y) result[y, x] = array[ix, y];
            }
            return new LandGen(result);
        }

    }

}
