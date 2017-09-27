using System;
using System.IO;
using Attributes;
using Commands.Server;
using DataTransfer.Data;


namespace DataTransfer.Server {

    [DTO(DTOCode.TurnDataServer)]
    public class TurnDataSCmd : ServerCommand {

        public TurnData Data { get; private set; }


        public override void ReadMembers (BinaryReader reader) {
            Data = new TurnData();
            Data.ReadMembers(reader);
        }


        public override void Execute () {
            CommandExecutor<TurnDataSCmd>.Execute(this);
        }

    }

}
