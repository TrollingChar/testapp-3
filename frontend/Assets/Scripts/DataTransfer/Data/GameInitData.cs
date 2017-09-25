using System.Collections.Generic;


namespace DataTransfer.Data {

    public class GameInitData {

        public List<int> Players;

        public int Seed;


        public GameInitData (int seed, List<int> players) {
            Seed = seed;
            Players = players;
        }

    }

}
