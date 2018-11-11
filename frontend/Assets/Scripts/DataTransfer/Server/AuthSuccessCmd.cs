using System;
using System.IO;
using Attributes;
using Core;
using Utils;


namespace DataTransfer.Server {

    [DTO (DTOCode.AuthSuccess)]
    public class AuthSuccessCmd : ServerCommand {

        public static event Action <AuthSuccessCmd> OnReceived;

        public PlayerInfo PlayerInfo { get; private set; }


        public override void ReadMembers (BinaryReader reader) {
            // todo
            PlayerInfo = new PlayerInfo (reader.ReadInt32 ());
        }


        public override void Execute () {
            The.PlayerInfo = PlayerInfo;
            OnReceived._ (this);
        }

    }

}