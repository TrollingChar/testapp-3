using System;
using System.IO;
using Attributes;


namespace DataTransfer.Server {

    [DTO(DTOCode.LeftGame)]
    public class LeftGameCmd : ServerCommand {

        protected override void ReadMembers (BinaryReader reader) {
            throw new NotImplementedException();
        }


        public override void Execute () {
            throw new NotImplementedException();
        }

    }

}
