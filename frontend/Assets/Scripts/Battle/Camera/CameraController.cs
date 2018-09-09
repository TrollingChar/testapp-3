using UnityEngine;
using Utils;


namespace Battle.Camera {

    public class CameraController {

        public CameraWrapper Camera;


        public virtual void OnAdd () {}
        public virtual void OnRemove () {}


        public virtual void Update () {
            if (Input.GetMouseButtonDown(MouseButtons.Right) && !Input.GetMouseButtonUp(MouseButtons.Right)) {
                Camera.Controller = new MouseBasedCameraController();
            }
        }

    }

}
