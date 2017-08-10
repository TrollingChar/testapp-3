using Geometry;
using UnityEngine;
using Utils.Singleton;
using War.Camera;
using Zenject;


namespace War {

    public class TurnData {

        [Inject] private CameraWrapper _camera;

        public bool W, A, S, D, MB;
        public XY XY;


        public TurnData () {
            W = Input.GetKey(KeyCode.W);
            A = Input.GetKey(KeyCode.A);
            S = Input.GetKey(KeyCode.S);
            D = Input.GetKey(KeyCode.D);
            MB = Input.GetMouseButton(0); // LMB
            XY = _camera.WorldMousePosition;
        }


        public TurnData (byte flags, float x, float y) {
            W = (flags & 0x01) != 0;
            A = (flags & 0x02) != 0;
            S = (flags & 0x04) != 0;
            D = (flags & 0x08) != 0;
            MB = (flags & 0x10) != 0;
            XY = new XY(x, y);
        }

    }

}
