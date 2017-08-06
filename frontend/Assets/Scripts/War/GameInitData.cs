using System.Collections.Generic;


namespace War {

    public class GameInitData {

        public int Seed;
        public List<int> Players;


        public GameInitData (int seed, List<int> players) {
            Seed = seed;
            Players = players;
        }

    }

}
