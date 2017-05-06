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
        camera.transform.position *= 0.7f;
        camera.transform.position += 0.3f * target;
        //Vector2 position = camera.transform.position;
        //position.x = 0.7f * camera.transform.position.x + 0.3f * target.x;
        //position.y = 0.7f * camera.transform.position.y + 0.3f * target.y;
        //camera.transform.position = position;
        Debug.Log(camera.ScreenToWorldPoint(Input.mousePosition).ToString());
	}

    public void LookAt(Vector2 xy) {
        
        //target = camera.WorldToScreenPoint(xy);

        //target.x = xy.x;
        //target.y = xy.y;
    }
}
