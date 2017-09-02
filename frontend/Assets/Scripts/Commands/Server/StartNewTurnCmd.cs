using Attributes;
using Net.Utils.IO;


namespace Commands.Server {

    [ServerCmd(ServerCmdId.StartNewTurn)]
    public class StartNewTurnCmd : IServerCommand {

        public int Player { get; private set; }


        public void Deserialize (EndianBinaryReader reader) {
            Player = reader.ReadInt32();
        }


        public void Execute () {
            CommandExecutor<StartNewTurnCmd>.Execute(this);
        }

    }

}
