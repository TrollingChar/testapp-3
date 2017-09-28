using System;
using System.IO;
using Attributes;
using Utils.Messenger;


namespace DataTransfer.Server {

    [DTO(DTOCode.LobbyStatus)]
    public class LobbyStatusCmd : ServerCommand {

        public int HubId { get; private set; }
        public int Players { get; private set; }
        public static readonly Messenger<LobbyStatusCmd> OnReceived = new Messenger<LobbyStatusCmd>();


        public override void ReadMembers (BinaryReader reader) {
            HubId = reader.ReadByte();
            Players = reader.ReadByte();
        }


        public override void Execute () {
            OnReceived.Send(this);
        }

    }

}
