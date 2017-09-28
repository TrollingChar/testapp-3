using System;
using System.IO;
using Attributes;
using Utils.Messenger;


namespace DataTransfer.Server {

    [DTO(DTOCode.NewTurn)]
    public class NewTurnCmd : ServerCommand {

        public static readonly Messenger<NewTurnCmd> OnReceived = new Messenger<NewTurnCmd>();
        public int Player { get; private set; }


        public override void ReadMembers (BinaryReader reader) {
            Player = reader.ReadInt32();
        }


        public override void Execute () {
            OnReceived.Send(this);
        }

    }

}
