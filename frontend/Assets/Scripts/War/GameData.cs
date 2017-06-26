using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3 {
    public class GameData {
        public int seed;
        public List<int> players;

        public GameData (int seed, List<int> players) {
            this.seed = seed;
            this.players = players;
        }
    }
}