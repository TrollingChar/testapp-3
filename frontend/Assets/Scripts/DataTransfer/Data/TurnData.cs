using System.IO;
using Attributes;
using Core;
using Geometry;
using UnityEngine;


namespace DataTransfer.Data {

    [DTO(DTOCode.TurnData)]
    public class TurnData : DTO {

        public bool W, A, S, D, MB; // byte - space reserved for Tab etc.
        public XY XY; // 2 floats
        public byte Weapon; // byte
        public byte NumKey; // 3 bits: 0-5


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
            byte numKey = 0;
            if (Input.GetKey(KeyCode.Alpha1)) numKey = 1;
            if (Input.GetKey(KeyCode.Alpha2)) numKey = 2;
            if (Input.GetKey(KeyCode.Alpha3)) numKey = 3;
            if (Input.GetKey(KeyCode.Alpha4)) numKey = 4;
            if (Input.GetKey(KeyCode.Alpha5)) numKey = 5;
            return new TurnData {
                W = Input.GetKey(KeyCode.W),
                A = Input.GetKey(KeyCode.A),
                S = Input.GetKey(KeyCode.S),
                D = Input.GetKey(KeyCode.D),
                MB = Input.GetMouseButton(0), // LMB
                XY = The.Camera.WorldMousePosition,
                Weapon = (byte) The.WeaponWrapper.PreparedId,
                NumKey = numKey
            };
        }


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
