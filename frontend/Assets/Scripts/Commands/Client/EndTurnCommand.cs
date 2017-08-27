using Attributes;
using Net.Utils.IO;


namespace Commands.Client {

    [ClientCommand(ClientAPI.EndTurn)]
    public class EndTurnCommand : IClientCommand {

        public void Serialize (EndianBinaryWriter writer) {}

    }

}
