using System;
using System.IO;
using Attributes;


namespace DataTransfer.Client {

    [DTO(DTOCode.JoinLobby)]
    public class JoinLobbyCmd : ClientCommand {

        protected override void WriteMembers (BinaryWriter writer) {
            throw new NotImplementedException();
        }

    }

}