using Attributes;
using Battle;
using Net.Utils.IO;


namespace Commands.Server {

    [ServerCmd(ServerAPI.HandleTurnData)]
    public class HandleTurnDataCommand : IServerCommand {

        public TurnData Data { get; private set; }


        public void Deserialize (EndianBinaryReader reader) {
        }


        public void Execute () {
            CommandExecutor<HandleTurnDataCommand>.Execute(this);
        }

    }

}
