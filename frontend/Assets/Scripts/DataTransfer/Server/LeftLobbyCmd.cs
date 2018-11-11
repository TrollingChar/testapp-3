using System;
using System.IO;
using Attributes;
using Utils;


namespace DataTransfer.Server {

    [DTO (DTOCode.LeftLobby)]
    public class LeftLobbyCmd : ServerCommand {

        public static event Action <LeftLobbyCmd> OnReceived;


        public override void ReadMembers (BinaryReader reader) {}


        public override void Execute () {
            OnReceived._ (this);
        }

    }

}