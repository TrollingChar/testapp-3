using Attributes;
using Net.Utils.IO;


namespace Commands.Client {

    [ClientCmd(ClientCmdId.EndTurn)]
    public class EndTurnCmd : ClientCommand {

        private readonly bool _alive;


        public EndTurnCmd (bool alive) {
            _alive = alive;
        }


        public override void Serialize (EndianBinaryWriter writer) {
            writer.WriteBoolean(_alive);
        }

    }

}
