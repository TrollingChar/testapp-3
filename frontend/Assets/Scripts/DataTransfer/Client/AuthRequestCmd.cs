using System;
using System.IO;
using Attributes;


namespace DataTransfer.Client {

    [DTO(DTOCode.AuthRequest)]
    public class AuthRequestCmd : ClientCommand {

        private readonly string _ip;
        private readonly int _id;


        public AuthRequestCmd (string ip, int id) {
            _ip = ip;
            _id = id;
        }


        protected override void WriteMembers (BinaryWriter writer) {
            writer.Write(_id);
        }

    }

}
