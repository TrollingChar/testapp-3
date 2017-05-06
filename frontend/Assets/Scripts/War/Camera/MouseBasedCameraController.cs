using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MouseBasedCameraController : CameraController {
    Vector2 clickXY;
    Vector3 origin;

    public MouseBasedCameraController (CameraWrapper cameraWrapper) : base(cameraWrapper) {
        //this.clickXY = camera.target + (Vector2)(Input.mousePosition);
        //origin = camera.camera.Sc
    }

    public override void Update () {
        camera.LookAt(clickXY - (Vector2)(Input.mousePosition));
        if (Input.GetMouseButtonUp(MouseButtons.Right)
        && !Input.GetMouseButtonDown(MouseButtons.Right))
            camera.controller = new CameraController(camera);
    }
}
