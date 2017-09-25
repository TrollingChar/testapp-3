using Attributes;
using Battle;
using DataTransfer.Data;
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
            writer.WriteSingle(_td.XY.X);
            writer.WriteSingle(_td.XY.Y);
            writer.WriteByte(_td.Weapon);
            writer.WriteByte(_td.Number);
        }

    }

}
