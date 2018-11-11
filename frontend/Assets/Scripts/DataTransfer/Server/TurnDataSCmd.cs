using System;
using System.IO;
using Attributes;
using DataTransfer.Data;
using Utils;


namespace DataTransfer.Server {

    [DTO (DTOCode.TurnDataServer)]
    public class TurnDataSCmd : ServerCommand {

        public static event Action <TurnDataSCmd> OnReceived;

        public TurnData Data { get; private set; }


        public override void ReadMembers (BinaryReader reader) {
            Data = new TurnData ();
            Data.ReadMembers (reader);
        }


        public override void Execute () {
            OnReceived._ (this);
        }

    }

}