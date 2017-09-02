using Attributes;
using Net.Utils.IO;


namespace Commands.Client {

    [ClientCmd(ClientCmdId.Authorize)]
    public class AuthorizeCmd : ClientCommand {

        public readonly string Ip;
        private readonly int _id;


        public AuthorizeCmd (string ip, int id) {
            Ip = ip;
            _id = id;
        }


        public override void Serialize (EndianBinaryWriter writer) {
            // don't write ip
            writer.WriteInt32(_id);
        }

    }

}
