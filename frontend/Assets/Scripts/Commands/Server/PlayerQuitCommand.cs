﻿using Attributes;
using Net.Utils.IO;


namespace Commands.Server {

    [ServerCommand(ServerAPI.PlayerLeftGame)]
    public class PlayerLeftGameCommand : IServerCommand {

        public void Deserialize (EndianBinaryReader reader) {
            throw new System.NotImplementedException();
        }


        public void Execute () {
            throw new System.NotImplementedException();
        }

    }

}