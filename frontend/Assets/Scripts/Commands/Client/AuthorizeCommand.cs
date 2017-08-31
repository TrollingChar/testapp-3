using Attributes;
using Net.Utils.IO;


namespace Commands.Client {

    [ClientCmd(ClientAPI.Authorize)]
    public class AuthorizeCommand : IClientCommand {

        private readonly int _ip;
        private readonly int _id;


        public AuthorizeCommand (int ip, int id) {
            _ip = ip;
            _id = id;
        }

        public void Serialize (EndianBinaryWriter writer) {
            writer.WriteInt32(_ip);
            writer.WriteInt32(_id);
        }

    }

}
