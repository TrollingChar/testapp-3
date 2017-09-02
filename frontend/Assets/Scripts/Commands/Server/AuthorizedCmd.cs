using Attributes;
using Core;
using Net.Utils.IO;


namespace Commands.Server {

    [ServerCmd(ServerCmdId.Authorized)]
    public class AuthorizedCmd : IServerCommand {

        public PlayerInfo PlayerInfo { get; private set; }


        public void Deserialize (EndianBinaryReader reader) {
            PlayerInfo = new PlayerInfo(reader.ReadInt32()); // todo: use DTO
        }


        public void Execute () {
            CommandExecutor<AuthorizedCmd>.Execute(this);
        }

    }

}
