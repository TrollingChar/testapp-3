using Attributes;
using Net.Utils.IO;


namespace Commands.Server {

    [ServerCmd(ServerAPI.NoWinner)]
    public class ShowNoWinnerCommand : IServerCommand {

        public void Deserialize (EndianBinaryReader reader) {}


        public void Execute () {
            CommandExecutor<ShowNoWinnerCommand>.Execute(this);
        }

    }

}
