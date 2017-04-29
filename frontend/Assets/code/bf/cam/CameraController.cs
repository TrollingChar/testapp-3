using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    new public Camera camera;
    Vector3 target = new Vector3();

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
        camera.transform.position = 0.7f * camera.transform.position + 0.3f * target;
	}

    public void LookAt(Vector2 xy) {
        target.x = xy.x;
        target.y = xy.y;
    }
}
