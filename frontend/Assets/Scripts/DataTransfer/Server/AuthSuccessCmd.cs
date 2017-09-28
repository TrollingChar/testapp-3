using System.IO;
using Attributes;
using Core;
using Utils.Singleton;

namespace DataTransfer.Server {

    [DTO(DTOCode.AuthSuccess)]
    public class AuthSuccessCmd : ServerCommand {

        public PlayerInfo PlayerInfo { get; private set; }


        public override void ReadMembers (BinaryReader reader) {
            // todo
            PlayerInfo = new PlayerInfo(reader.ReadInt32());
        }


        public override void Execute () {
//            CommandExecutor<AuthSuccessCmd>.Execute(this);
            The<PlayerInfo>.Set(PlayerInfo);
        }

    }

}
