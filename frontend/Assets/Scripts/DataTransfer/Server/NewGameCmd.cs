using System;
using System.IO;
using Attributes;


namespace DataTransfer.Server {

    [DTO(DTOCode.NewGame)]
    public class NewGameCmd: ServerCommand {

        protected override void ReadMembers (BinaryReader reader) {
            throw new NotImplementedException();
        }


        public override void Execute () {
            throw new NotImplementedException();
        }

    }

}