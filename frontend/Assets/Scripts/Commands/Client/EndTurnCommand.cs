using Attributes;
using Net.Utils.IO;


namespace Commands.Client {

    [ClientCmd(ClientAPI.EndTurn)]
    public class EndTurnCommand : IClientCommand {

        public void Serialize (EndianBinaryWriter writer) {}

    }

}
