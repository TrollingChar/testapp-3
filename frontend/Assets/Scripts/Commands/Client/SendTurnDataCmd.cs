using Attributes;
using Battle;
using Net.Utils.IO;


namespace Commands.Client {

    [ClientCmd(ClientCmdId.SendTurnData)]
    public class SendTurnDataCmd : ClientCommand {

        private readonly TurnData _td;


        public SendTurnDataCmd (TurnData td) {
            _td = td;
        }


        public override void Serialize (EndianBinaryWriter writer) {
            // todo: use DTO
            writer.WriteByte(_td.Flags);
            writer.WriteFloat(_td.XY.X);
            writer.WriteFloat(_td.XY.Y);
        }

    }

}
