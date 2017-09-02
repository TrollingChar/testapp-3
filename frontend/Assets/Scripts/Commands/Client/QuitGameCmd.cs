using Attributes;
using Net.Utils.IO;


namespace Commands.Client {

    [ClientCmd(ClientCmdId.QuitGame)]
    public class QuitGameCmd : ClientCommand {

        public override void Serialize (EndianBinaryWriter writer) {}

    }

}
