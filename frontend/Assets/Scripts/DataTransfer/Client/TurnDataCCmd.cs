using System;
using System.IO;
using Attributes;
using DataTransfer.Data;


namespace DataTransfer.Client {

    [DTO(DTOCode.TurnDataClient)]
    public class TurnDataCCmd : ClientCommand {

        public TurnDataCCmd (TurnData data) {
            Data = data;
        }


        public TurnData Data { get; private set; }


        protected override void WriteMembers (BinaryWriter writer) {
            throw new NotImplementedException();
        }

    }

}
