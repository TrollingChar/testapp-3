using System.IO;
using Attributes;


namespace DataTransfer.Client {

    [DTO(DTOCode.LeaveLobby)]
    public class LeaveLobbyCmd : ClientCommand {

        public override void WriteMembers (BinaryWriter writer) {}

    }

}
