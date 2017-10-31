using System.IO;
using Attributes;
using Core;
using Utils.Messenger;


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
            The.PlayerInfo = PlayerInfo;
            OnReceived.Send(this);
        }

    }

}
