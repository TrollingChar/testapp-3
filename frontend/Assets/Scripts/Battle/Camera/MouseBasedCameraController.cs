using UnityEngine;
using Utils;


namespace Battle.Camera {

    public class MouseBasedCameraController : CameraController {

        private readonly Vector3 _click;
        private readonly Vector3 _origin;


        public MouseBasedCameraController (CameraWrapper camera) : base(camera) {
            _click = Input.mousePosition;
            _origin = camera.Target;
        }


        public override void Update () {
            Camera.LookAt(
                _origin + Camera.Camera.ScreenToWorldPoint(_click)
                - Camera.Camera.ScreenToWorldPoint(Input.mousePosition)
            );
            if (Input.GetMouseButtonUp(MouseButtons.Right)
                && !Input.GetMouseButtonDown(MouseButtons.Right)
            ) {
                Camera.Controller = new CameraController(Camera);
            }
        }

    }

}
