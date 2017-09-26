using System;
using System.IO;
using Attributes;


namespace DataTransfer.Server {

    [DTO(DTOCode.GameEnded)]
    public class GameEndedCmd : ServerCommand {

        protected override void ReadMembers (BinaryReader reader) {
            throw new NotImplementedException();
        }


        public override void Execute () {
            throw new NotImplementedException();
        }

    }

}
