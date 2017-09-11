using Attributes;
using Net.Utils.IO;


namespace Commands.Server {

    [ServerCmd(ServerCmdId.UpdateLobbyStatus)]
    public class UpdateLobbyStatusCmd : IServerCommand {

        public byte HubId { get; private set; }
        public byte Players { get; private set; }


        public void Deserialize (EndianBinaryReader reader) {
            HubId = reader.ReadByte();
            Players = reader.ReadByte();
        }


        public void Execute () {
            CommandExecutor<UpdateLobbyStatusCmd>.Execute(this);
        }

    }

}
