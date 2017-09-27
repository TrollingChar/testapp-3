using System.IO;
using Attributes;
using Commands.Server;


namespace DataTransfer.Server {

    [DTO(DTOCode.LeftLobby)]
    public class LeftLobbyCmd : ServerCommand {

        public override void ReadMembers (BinaryReader reader) {}


        public override void Execute () {
            CommandExecutor<LeftLobbyCmd>.Execute(this);
        }

    }

}
