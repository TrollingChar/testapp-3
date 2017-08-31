using Attributes;
using Net.Utils.IO;


namespace Commands.Client {

    [ClientCmd(ClientAPI.QuitGame)]
    public class QuitGameCommand : IClientCommand {

        public void Serialize (EndianBinaryWriter writer) {}

    }

}
