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
            // todo: use DTO
            writer.WriteByte(_td.Flags);
            writer.WriteFloat(_td.XY.X);
            writer.WriteFloat(_td.XY.Y);
        }

    }

}
