using Battle.Camera;
using Geometry;
using UnityEngine;
using Utils.Singleton;


namespace Battle {

    public class TurnData {

        public bool W, A, S, D, MB; // byte - space reserved for Tab etc.
        public XY XY;               // 2 floats
        public byte Weapon;         // byte
        public byte Number;         // 3 bits: 0-5


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
        }


        public TurnData () {
            W = Input.GetKey(KeyCode.W);
            A = Input.GetKey(KeyCode.A);
            S = Input.GetKey(KeyCode.S);
            D = Input.GetKey(KeyCode.D);
            MB = Input.GetMouseButton(0); // LMB
            XY = The<CameraWrapper>.Get().WorldMousePosition;
            Weapon = Number = 0;
        }


        public TurnData (byte flags, float x, float y, byte weapon, byte number) {
            W = (flags & 0x01) != 0;
            A = (flags & 0x02) != 0;
            S = (flags & 0x04) != 0;
            D = (flags & 0x08) != 0;
            MB = (flags & 0x10) != 0;
            XY = new XY(x, y);
            Weapon = weapon;
            Number = number;
        }

    }

}
