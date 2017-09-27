using System.IO;
using Attributes;


namespace DataTransfer.Client {

    [DTO(DTOCode.JoinLobby)]
    public class JoinLobbyCmd : ClientCommand {

        public byte LobbyId { get; private set; }


        public JoinLobbyCmd (byte lobbyId) {
            LobbyId = lobbyId;
        }


        public override void WriteMembers (BinaryWriter writer) {
            writer.Write(LobbyId);
        }

    }

}
