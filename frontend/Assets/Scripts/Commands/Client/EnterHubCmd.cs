using Attributes;
using Net;
using Net.Utils.IO;
using Utils.Singleton;


namespace Commands.Client {

    [ClientCmd(ClientCmdId.EnterHub)]
    public class EnterHubCmd : ClientCommand {

        private readonly byte _hubId;


        public EnterHubCmd (byte hubId) {
            _hubId = hubId;
        }


        public override void Serialize (EndianBinaryWriter writer) {
            writer.WriteByte(_hubId);
        }

    }

}
