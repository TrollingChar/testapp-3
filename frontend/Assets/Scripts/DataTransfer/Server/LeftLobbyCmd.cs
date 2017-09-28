using System.IO;
using Attributes;
using Commands.Server;
using Utils.Messenger;


namespace DataTransfer.Server {

    [DTO(DTOCode.LeftLobby)]
    public class LeftLobbyCmd : ServerCommand {

        public static readonly Messenger<LeftLobbyCmd> OnReceived = new Messenger<LeftLobbyCmd>();
            
        
        public override void ReadMembers (BinaryReader reader) {}


        public override void Execute () {
            OnReceived.Send(this);
        }

    }

}
