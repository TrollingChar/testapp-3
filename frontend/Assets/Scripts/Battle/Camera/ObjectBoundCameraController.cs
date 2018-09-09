using Geometry;
using UnityEngine;
using Object = Battle.Objects.Object;


namespace Battle.Camera {

    public class ObjectBoundCameraController : CameraController {

        private readonly Object _object;
        private readonly float _coeff;


        public ObjectBoundCameraController (Object obj, float coeff = 0) {
            _object = obj;
            _coeff = coeff;
        }


        public override void Update () {
            var camera = Camera.Camera;
            var cursor = (XY) (Vector2) Input.mousePosition;
            var offset = cursor - 0.5f * new XY (camera.pixelWidth, camera.pixelHeight);
            Camera.LookAt (_object.Position + offset * _coeff);
            
            base.Update ();
        }

    }

}
