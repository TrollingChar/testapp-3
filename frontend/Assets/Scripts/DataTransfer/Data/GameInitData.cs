using System;
using System.Collections.Generic;
using System.IO;
using Attributes;


namespace DataTransfer.Data {

    [DTO(DTOCode.GameInitData)]
    public class GameInitData : DTO {

        public int Seed;
        public List<int> Players;


        public GameInitData () {
            Players = new List<int>();
        }


        public override void ReadMembers (BinaryReader reader) {
            Seed = reader.ReadInt32();
            for (int i = 0, count = reader.ReadByte(); i < count; i++) {
                Players.Add(reader.ReadInt32());
            }
        }


        public override void WriteMembers (BinaryWriter writer) {
            throw new Exception("Attempt to serialize a DTO which can only come from server!");
        }

    }

}
