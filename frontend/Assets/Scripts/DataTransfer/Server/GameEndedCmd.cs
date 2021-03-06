using System;
using System.IO;
using Attributes;
using Utils;


namespace DataTransfer.Server {

    [DTO (DTOCode.GameEnded)]
    public class GameEndedCmd : ServerCommand {

        public const byte Draw    = 0;
        public const byte Victory = 1;
        public const byte Desync  = 255;

        public byte Result   { get; private set; }
        public int  PlayerId { get; private set; }

        public static event Action <GameEndedCmd> OnReceived;


        public override void ReadMembers (BinaryReader reader) {
            Result = reader.ReadByte ();
            if (Result == Victory) PlayerId = reader.ReadInt32 ();
        }


        public override void Execute () {
            OnReceived._ (this);
        }

    }

}