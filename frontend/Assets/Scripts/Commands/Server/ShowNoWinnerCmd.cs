using Attributes;
using Net.Utils.IO;


namespace Commands.Server {

    [ServerCmd(ServerCmdId.NoWinner)]
    public class ShowNoWinnerCmd : IServerCommand {

        public void Deserialize (EndianBinaryReader reader) {}


        public void Execute () {
            CommandExecutor<ShowNoWinnerCmd>.Execute(this);
        }

    }

}
