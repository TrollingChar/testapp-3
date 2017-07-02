using UnityEngine;


namespace War.Camera {

    public class AutomaticCameraController : CameraController {

        public AutomaticCameraController (CameraWrapper camera) : base(camera) {}


        public override void Update () {
            camera.LookAt(new Vector3(1000, 0, 0));
        }

    }

}
