using System.Collections.Generic;


namespace Battle {

    public class GameInitData {

        public List<int> Players;

        public int Seed;


        public GameInitData (int seed, List<int> players) {
            Seed = seed;
            Players = players;
        }

    }

}
