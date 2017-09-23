using System;
using System.IO;
using Attributes;


namespace DataTransfer.Client {

    [DTO(DTOCode.TurnDataClient)]
    public class TurnDataCCmd : ClientCommand {

        protected override void WriteMembers (BinaryWriter writer) {
            throw new NotImplementedException();
        }

    }

}
