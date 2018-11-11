using Core;
using UnityEngine;
using UnityEngine.UI;


namespace Utils {

    public class CanvasCameraAutoSetter : MonoBehaviour {

        private void Awake () {
            GetComponent <Canvas> ().worldCamera = The.Camera.Camera;
            Destroy (this);
        }

    }

}