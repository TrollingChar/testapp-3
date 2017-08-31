using Attributes;
using Net.Utils.IO;


namespace Commands.Server {

    [ServerCmd(ServerAPI.StartNewTurn)]
    public class StartNewTurnCommand : IServerCommand {

        public int Player { get; private set; }


        public void Deserialize (EndianBinaryReader reader) {
            Player = reader.ReadInt32();
        }


        public void Execute () {
            CommandExecutor<StartNewTurnCommand>.Execute(this);
        }

    }

}
