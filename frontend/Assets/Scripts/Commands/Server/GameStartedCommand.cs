using Attributes;
using Battle;
using Net.Utils.IO;


namespace Commands.Server {

    [ServerCommand(ServerAPI.GameStarted)]
    public class GameStartedCommand : IServerCommand {

        public GameInitData Data { get; private set; }


        public void Deserialize (EndianBinaryReader reader) {
            Data = reader.Read<GameInitData>();
        }


        public void Execute () {
            CommandExecutor<GameStartedCommand>.Execute(this);
        }

    }

}
