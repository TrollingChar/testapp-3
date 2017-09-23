using System;
using System.IO;
using Attributes;


namespace DataTransfer.Server {

    [DTO(DTOCode.LobbyStatus)]
    public class LobbyStatusCmd : ServerCommand {

        protected override void ReadMembers (BinaryReader reader) {
            throw new NotImplementedException();
        }


        public override void Execute () {
            throw new NotImplementedException();
        }

    }

}