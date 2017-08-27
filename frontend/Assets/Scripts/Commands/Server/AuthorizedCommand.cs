using Attributes;
using Core;
using Net.Utils.IO;


namespace Commands.Server {

    [ServerCommand(ServerAPI.Authorized)]
    public class AuthorizedCommand : IServerCommand {

        public PlayerInfo PlayerInfo { get; private set; }

        public void Deserialize (EndianBinaryReader reader) {
            PlayerInfo = new PlayerInfo(reader.ReadInt32()); // todo: use DTO
        }


        public void Execute () {
            CommandExecutor<AuthorizedCommand>.Execute(this);
        }

    }

}