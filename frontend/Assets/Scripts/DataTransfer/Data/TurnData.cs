using System.IO;
using Attributes;
using Battle.Camera;
using Battle.State;
using Geometry;
using UnityEngine;
using Utils.Singleton;


namespace DataTransfer.Data {

    [DTO(DTOCode.TurnData)]
    public class TurnData : DTO {

        public bool W, A, S, D, MB; // byte - space reserved for Tab etc.
        public XY XY;               // 2 floats
        public byte Weapon;         // byte
        public byte NumKey;         // 3 bits: 0-5


        public byte Flags {
            get {
                return (byte) (
                    (W ? 0x01 : 0) |
                    (A ? 0x02 : 0) |
                    (S ? 0x04 : 0) |
                    (D ? 0x08 : 0) |
                    (MB ? 0x10 : 0)
                );
            }
            set {
                W = (value & 0x01) != 0;
                A = (value & 0x02) != 0;
                S = (value & 0x04) != 0;
                D = (value & 0x08) != 0;
                MB = (value & 0x10) != 0;
            }
        }
        

        public static TurnData FromInput () {
            return new TurnData {
                W = Input.GetKey(KeyCode.W),
                A = Input.GetKey(KeyCode.A),
                S = Input.GetKey(KeyCode.S),
                D = Input.GetKey(KeyCode.D),
                MB = Input.GetMouseButton(0), // LMB
                XY = The<CameraWrapper>.Get().WorldMousePosition,
                Weapon = (byte) The<WeaponWrapper>.Get().PreparedId,
                NumKey = 0
            };
        }


//        public TurnData (byte flags, float x, float y, byte weapon, byte numKey) {
//            W = (flags & 0x01) != 0;
//            A = (flags & 0x02) != 0;
//            S = (flags & 0x04) != 0;
//            D = (flags & 0x08) != 0;
//            MB = (flags & 0x10) != 0;
//            XY = new XY(x, y);
//            Weapon = weapon;
//            NumKey = numKey;
//        }


        public override void ReadMembers (BinaryReader reader) {
            Flags = reader.ReadByte();
            XY = new XY(reader.ReadSingle(), reader.ReadSingle());
            Weapon = reader.ReadByte();
            NumKey = reader.ReadByte();
        }


        public override void WriteMembers (BinaryWriter writer) {
            writer.Write(Flags);
            writer.Write(XY.X);
            writer.Write(XY.Y);
            writer.Write(Weapon);
            writer.Write(NumKey);
        }

    }

}
