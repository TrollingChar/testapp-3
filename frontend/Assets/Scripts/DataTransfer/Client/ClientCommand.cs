using System;
using System.IO;


namespace DataTransfer.Client {

    public abstract class ClientCommand : DTO {

        protected override void ReadMembers (BinaryReader reader) {
            throw new NotSupportedException("Attempt to deserialize a client command!");
        }

    }

}
