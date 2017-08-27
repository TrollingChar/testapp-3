using Attributes;
using Net.Utils.IO;


namespace Commands.Client {

    [ClientCommand(ClientAPI.QuitGame)]
    public class QuitGameCommand : IClientCommand {

        public void Serialize (EndianBinaryWriter writer) {}

    }

}
