using System.Collections.Generic;


namespace War {

    public class GameData {

        public int seed;
        public List<int> players;


        public GameData (int seed, List<int> players) {
            this.seed = seed;
            this.players = players;
        }

    }

}
