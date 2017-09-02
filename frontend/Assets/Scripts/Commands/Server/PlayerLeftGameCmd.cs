using Attributes;
using Net.Utils.IO;


namespace Commands.Server {

    [ServerCmd(ServerCmdId.PlayerLeftGame)]
    public class PlayerLeftGameCmd : IServerCommand {

        public void Deserialize (EndianBinaryReader reader) {
            throw new System.NotImplementedException();
        }


        public void Execute () {
            throw new System.NotImplementedException();
        }

    }

}
