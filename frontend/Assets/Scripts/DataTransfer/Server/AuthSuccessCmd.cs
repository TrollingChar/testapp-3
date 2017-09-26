using System;
using System.IO;
using Attributes;
using Commands.Server;
using Core;


namespace DataTransfer.Server {

    [DTO(DTOCode.AuthSuccess)]
    public class AuthSuccessCmd : ServerCommand {

        public PlayerInfo PlayerInfo { get; private set; }


        protected override void ReadMembers (BinaryReader reader) {
            // todo
            PlayerInfo = new PlayerInfo(reader.ReadInt32());
        }


        public override void Execute () {
            CommandExecutor<AuthSuccessCmd>.Execute(this);
        }

    }

}
