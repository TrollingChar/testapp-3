using System;
using System.IO;
using Attributes;
using DataTransfer.Data;
using Utils.Messenger;


namespace DataTransfer.Server {

    [DTO(DTOCode.TurnDataServer)]
    public class TurnDataSCmd : ServerCommand {

        public static readonly Messenger<TurnDataSCmd> OnReceived = new Messenger<TurnDataSCmd>();
        public TurnData Data { get; private set; }


        public override void ReadMembers (BinaryReader reader) {
            Data = new TurnData();
            Data.ReadMembers(reader);
        }


        public override void Execute () {
            OnReceived.Send(this);
        }

    }

}
