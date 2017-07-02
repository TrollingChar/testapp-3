using System.Collections.Generic;


namespace War {

    public class GameData {

        public int Seed;
        public List<int> Players;


        public GameData (int seed, List<int> players) {
            this.Seed = seed;
            this.Players = players;
        }

    }

}
