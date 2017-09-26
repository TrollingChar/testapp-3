using System.IO;
using Attributes;


namespace DataTransfer.Client {

    [DTO(DTOCode.AuthRequest)]
    public class AuthRequestCmd : ClientCommand {

        public AuthRequestCmd (string ip, int id) {
            Ip = ip;
            Id = id;
        }


        public int Id { get; private set; }
        public string Ip { get; private set; }


        protected override void WriteMembers (BinaryWriter writer) {
            writer.Write(Id);
        }

    }

}
