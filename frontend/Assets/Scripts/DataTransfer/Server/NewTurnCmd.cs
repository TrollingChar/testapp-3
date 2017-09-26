using System;
using System.IO;
using Attributes;
using Commands.Server;


namespace DataTransfer.Server {

    [DTO(DTOCode.NewTurn)]
    public class NewTurnCmd : ServerCommand {

        public int Player { get; private set; }


        protected override void ReadMembers (BinaryReader reader) {
            Player = reader.ReadInt32();
        }


        public override void Execute () {
            CommandExecutor<NewTurnCmd>.Execute(this);
        }

    }

}
