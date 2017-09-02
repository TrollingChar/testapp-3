using Attributes;
using Battle;
using Net.Utils.IO;


namespace Commands.Server {

    [ServerCmd(ServerCmdId.HandleTurnData)]
    public class HandleTurnDataCmd : IServerCommand {

        public TurnData Data { get; private set; }


        public void Deserialize (EndianBinaryReader reader) {}


        public void Execute () {
            CommandExecutor<HandleTurnDataCmd>.Execute(this);
        }

    }

}
