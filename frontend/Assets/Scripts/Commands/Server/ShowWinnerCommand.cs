using Attributes;
using Net.Utils.IO;


namespace Commands.Server {

    [ServerCmd(ServerAPI.ShowWinner)]
    public class ShowWinnerCommand : IServerCommand {
    
        public int Winner { get; private set; }


        public void Deserialize (EndianBinaryReader reader) {
            Winner = reader.ReadInt32();
        }


        public void Execute () {
            CommandExecutor<ShowWinnerCommand>.Execute(this);
        }

    }

}
