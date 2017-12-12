using System.IO;
using Attributes;


namespace DataTransfer.Client {

    [DTO(DTOCode.LeaveGame)]
    public class LeaveGameCmd : ClientCommand {

        public override void WriteMembers (BinaryWriter writer) {}

    }

}
