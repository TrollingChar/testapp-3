using System.IO;
using Attributes;
using Core;
using DataTransfer.Data;


namespace DataTransfer.Server {

    [DTO (DTOCode.NewGame)]
    public class NewGameCmd : ServerCommand {

        public GameInitData Data { get; private set; }


        public override void ReadMembers (BinaryReader reader) {
            Data = new GameInitData ();
            Data.ReadMembers (reader);
        }


        public override void Execute () {
            The.SceneSwitcher.Load (Scenes.Battle, Data);
        }

    }

}