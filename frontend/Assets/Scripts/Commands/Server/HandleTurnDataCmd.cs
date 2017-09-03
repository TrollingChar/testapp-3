using Attributes;
using Battle;
using Net.Utils.IO;


namespace Commands.Server {

    [ServerCmd(ServerCmdId.HandleTurnData)]
    public class HandleTurnDataCmd : IServerCommand {

        public TurnData Data { get; private set; }


        public void Deserialize (EndianBinaryReader reader) {
            // todo: use DTO
            Data = new TurnData(
                reader.ReadByte(),
                reader.ReadSingle(),
                reader.ReadSingle(),
                reader.ReadByte(),
                reader.ReadByte()
            );
        }


        public void Execute () {
            CommandExecutor<HandleTurnDataCmd>.Execute(this);
        }

    }

}
