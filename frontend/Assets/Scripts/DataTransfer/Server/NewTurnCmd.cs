using System;
using System.IO;
using Attributes;
using Utils;


namespace DataTransfer.Server {

    [DTO (DTOCode.NewTurn)]
    public class NewTurnCmd : ServerCommand {

        public static event Action <NewTurnCmd> OnReceived;

        public int Player { get; private set; }


        public override void ReadMembers (BinaryReader reader) {
            Player = reader.ReadInt32 ();
        }


        public override void Execute () {
            OnReceived._ (this);
        }

    }

}