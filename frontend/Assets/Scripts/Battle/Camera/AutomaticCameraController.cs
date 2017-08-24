using UnityEngine;


namespace Battle.Camera {

    public class AutomaticCameraController : CameraController {

        public AutomaticCameraController (CameraWrapper camera) : base(camera) {}


        public override void Update () {
            Camera.LookAt(new Vector3(1000, 0, 0));
        }

    }

}
