using UnityEngine;


namespace W3 {

    public class MouseBasedCameraController : CameraController {

        private Vector3 origin, click;


        public MouseBasedCameraController (CameraWrapper camera) : base(camera) {
            click = Input.mousePosition;
            origin = camera.target;
        }


        public override void Update () {
            camera.LookAt(
                origin + camera.camera.ScreenToWorldPoint(click)
                - camera.camera.ScreenToWorldPoint(Input.mousePosition)
            );
            if (Input.GetMouseButtonUp(MouseButtons.Right)
                && !Input.GetMouseButtonDown(MouseButtons.Right)
            ) {
                camera.controller = new CameraController(camera);
            }
        }

    }

}
