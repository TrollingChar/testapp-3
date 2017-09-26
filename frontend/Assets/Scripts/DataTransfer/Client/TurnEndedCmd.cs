using System;
using System.IO;
using Attributes;


namespace DataTransfer.Client {

    [DTO(DTOCode.TurnEnded)]
    public class TurnEndedCmd : ClientCommand {

        protected override void WriteMembers (BinaryWriter writer) {
            throw new NotImplementedException();
        }

    }

}
