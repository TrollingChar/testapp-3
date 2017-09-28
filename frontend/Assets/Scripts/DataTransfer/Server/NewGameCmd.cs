using System;
using System.IO;
using Attributes;
using Commands.Server;
using Core;
using DataTransfer.Data;
using Utils.Singleton;


namespace DataTransfer.Server {

    [DTO(DTOCode.NewGame)]
    public class NewGameCmd : ServerCommand {

        public GameInitData Data { get; private set; }


        public override void ReadMembers (BinaryReader reader) {
            Data = new GameInitData();
            Data.ReadMembers(reader);
        }


        public override void Execute () {
            The<SceneSwitcher>.Get().Load(Scenes.Battle, Data);
        }

    }

}
