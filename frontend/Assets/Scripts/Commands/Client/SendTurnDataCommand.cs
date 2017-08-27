using Attributes;
using Battle;
using Net.Utils.IO;


namespace Commands.Client {

    [ClientCommand(ClientAPI.SendTurnData)]
    public class SendTurnDataCommand : IClientCommand {

        private TurnData _td;


        public SendTurnDataCommand (TurnData td) {
            _td = td;
        }


        public void Serialize (EndianBinaryWriter writer) {
            writer.Write(_td);
        }

    }

}
