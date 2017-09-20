using Attributes;
using Net.Utils.IO;


namespace Commands.Server {

    [ServerCmd(ServerCmdId.RemovedFromLobby)]
    public class RemovedFromLobbyCmd : IServerCommand {

        public void Deserialize (EndianBinaryReader reader) {}


        public void Execute () {
            CommandExecutor<RemovedFromLobbyCmd>.Execute(this);
        }

    }

}
