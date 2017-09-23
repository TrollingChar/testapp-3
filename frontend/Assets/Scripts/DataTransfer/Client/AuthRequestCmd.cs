using System;
using System.IO;
using Attributes;


namespace DataTransfer.Client {

    [DTO(DTOCode.AuthRequest)]
    public class AuthRequestCmd : ClientCommand {

        protected override void WriteMembers (BinaryWriter writer) {
            throw new NotImplementedException();
        }

    }

}