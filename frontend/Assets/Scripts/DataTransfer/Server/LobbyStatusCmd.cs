using System;
using System.IO;
using Attributes;
using Commands.Server;


namespace DataTransfer.Server {

    [DTO(DTOCode.LobbyStatus)]
    public class LobbyStatusCmd : ServerCommand {

        public int HubId { get; private set; }
        public int Players { get; private set; }


        public override void ReadMembers (BinaryReader reader) {
            HubId = reader.ReadByte();
            Players = reader.ReadByte();
        }


        public override void Execute () {
            CommandExecutor<LobbyStatusCmd>.Execute(this);
        }

    }

}
