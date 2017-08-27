using Attributes;
using Net.Utils.IO;


namespace Commands.Server {

    [ServerCommand(ServerAPI.NoWinner)]
    public class ShowNoWinnerCommand : IServerCommand {

        public void Deserialize (EndianBinaryReader reader) {
            throw new System.NotImplementedException();
        }


        public void Execute () {
            throw new System.NotImplementedException();
        }

    }

}