using System;
using System.IO;


namespace DataTransfer.Server {

    public abstract class ServerCommand : DTO {

        protected override void WriteMembers (BinaryWriter writer) {
            throw new NotSupportedException("Attempt to serialize a server command!");
        }


        public abstract void Execute ();

    }

}
