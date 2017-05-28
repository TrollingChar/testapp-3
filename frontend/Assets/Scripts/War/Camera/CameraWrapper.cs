using UnityEngine;

namespace W3 {
    public class CameraWrapper : MonoBehaviour {
        [HideInInspector] new public UnityEngine.Camera camera;
        [HideInInspector] public Vector3 target;
        [HideInInspector] public CameraController controller;

        void Start () {
            camera = gameObject.GetComponent<UnityEngine.Camera>();
            target = camera.transform.position;
            controller = new CameraController(this);
        }

        void Update () {
            controller.Update();
            camera.transform.position = Vector3.LerpUnclamped(camera.transform.position, target, 0.2f);
        }

        void FixedUpdate () {
            //cameraWrapper.transform.position *= 0.8f;
            //cameraWrapper.transform.position += 0.2f * target;
        }

        public void LookAt (Vector3 xyz) {
            target.x = xyz.x;
            target.y = xyz.y;
        }
    }
}