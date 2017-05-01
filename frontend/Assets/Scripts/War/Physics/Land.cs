using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Land {
    byte[,] array;
    LandTiles tiles;

    public Land (LandGen gen) {
        array = gen.array;
    }
}
