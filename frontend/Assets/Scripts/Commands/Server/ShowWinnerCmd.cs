using Attributes;
using Net.Utils.IO;


namespace Commands.Server {

    [ServerCmd(ServerCmdId.ShowWinner)]
    public class ShowWinnerCmd : IServerCommand {

        public int Winner { get; private set; }


        public void Deserialize (EndianBinaryReader reader) {
            Winner = reader.ReadInt32();
        }


        public void Execute () {
            CommandExecutor<ShowWinnerCmd>.Execute(this);
        }

    }

}
