using System.IO;
using Attributes;
using Commands.Server;


namespace DataTransfer.Server {

    [DTO(DTOCode.LeftLobby)]
    public class LeftLobbyCmd : ServerCommand {

        protected override void ReadMembers (BinaryReader reader) {}


        public override void Execute () {
            CommandExecutor<LeftLobbyCmd>.Execute(this);
        }

    }

}
