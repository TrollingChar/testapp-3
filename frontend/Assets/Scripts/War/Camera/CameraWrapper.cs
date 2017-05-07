using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWrapper : MonoBehaviour {
    new public Camera camera;
    [HideInInspector] public Vector3 target;
    [HideInInspector] public CameraController controller;
    
    void Start () {
        target = camera.transform.position;
        controller = new CameraController(this);
    }
	
	void Update () {
        controller.Update();
        camera.transform.position *= 0.8f;
        camera.transform.position += 0.2f * target;
	}

    public void LookAt(Vector3 xyz) {
        target.x = xyz.x;
        target.y = xyz.y;
    }
}
