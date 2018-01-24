using System.IO;
using Attributes;
using UnityEngine;


namespace DataTransfer.Client {

    [DTO(DTOCode.TurnEnded)]
    public class TurnEndedCmd : ClientCommand {

        public bool Alive { get; private set; }


        public TurnEndedCmd (bool alive) {
            Alive = alive;
        }


        public override void WriteMembers (BinaryWriter writer) {
            writer.Write(Alive);
        }

    }

}
