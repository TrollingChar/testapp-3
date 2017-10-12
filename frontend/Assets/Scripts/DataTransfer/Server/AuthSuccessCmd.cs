using System.IO;
using Attributes;
using Core;
using Utils.Messenger;
using Utils.Singleton;


namespace DataTransfer.Server {

    [DTO(DTOCode.AuthSuccess)]
    public class AuthSuccessCmd : ServerCommand {

        public static readonly Messenger<AuthSuccessCmd> OnReceived = new Messenger<AuthSuccessCmd>();
        public PlayerInfo PlayerInfo { get; private set; }


        public override void ReadMembers (BinaryReader reader) {
            // todo
            PlayerInfo = new PlayerInfo(reader.ReadInt32());
        }


        public override void Execute () {
            The<PlayerInfo>.Set(PlayerInfo);
            OnReceived.Send(this);
        }

    }

}
