using System;
using System.Collections.Generic;
using System.IO;


namespace DataTransfer.Data {

    public class GameInitData : DTO {

        public int Seed;
        public List<int> Players;


        public GameInitData () {
            Players = new List<int>();
        }
        

//        public GameInitData (int seed, List<int> players) {
//            Seed = seed;
//            Players = players;
//        }


        public override void ReadMembers (BinaryReader reader) {
            Seed = reader.ReadInt32();
            for (int i = 0; i < reader.ReadByte(); i++) {
                Players.Add(reader.ReadInt32());
            }
        }


        public override void WriteMembers (BinaryWriter writer) {
            throw new Exception("Attempt to serialize a DTO which can only come from server!");
        }

    }

}
