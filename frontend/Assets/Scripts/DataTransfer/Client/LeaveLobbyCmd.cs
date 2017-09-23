using System;
using System.IO;
using Attributes;


namespace DataTransfer.Client {

    [DTO(DTOCode.LeaveLobby)]
    public class LeaveLobbyCmd : ClientCommand {

        protected override void WriteMembers (BinaryWriter writer) {
            throw new NotImplementedException();
        }

    }

}