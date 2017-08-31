using Attributes;
using Net.Utils.IO;


namespace Commands.Client {

    [ClientCmd(ClientAPI.EnterHub)]
    public class EnterHubCommand : IClientCommand {

        private byte _hubId;


        public EnterHubCommand (byte hubId) {
            _hubId = hubId;
        }


        public void Serialize (EndianBinaryWriter writer) {
            writer.WriteByte(_hubId);
        }

    }

}
