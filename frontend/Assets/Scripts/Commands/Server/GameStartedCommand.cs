using System.Collections.Generic;
using Attributes;
using Battle;
using Net.Utils.IO;


namespace Commands.Server {

    [ServerCommand(ServerAPI.GameStarted)]
    public class GameStartedCommand : IServerCommand {

        public GameInitData Data { get; private set; }


        public void Deserialize (EndianBinaryReader reader) {
            // todo: use DTO
            int seed = reader.ReadInt32();
            var players = new List<int>();
            for (byte i = 0, end = reader.ReadByte(); i < end; ++i) players.Add(reader.ReadInt32());
            Data = new GameInitData(seed, players);
        }


        public void Execute () {
            CommandExecutor<GameStartedCommand>.Execute(this);
        }

    }

}
