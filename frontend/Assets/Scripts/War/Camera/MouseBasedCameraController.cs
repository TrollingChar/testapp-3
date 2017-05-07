using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MouseBasedCameraController : CameraController {
    Vector3 origin, click;

    public MouseBasedCameraController (CameraWrapper cameraWrapper) : base(cameraWrapper) {
        click = Input.mousePosition;
        origin = camera.target;
    }

    public override void Update () {
        camera.LookAt(origin
                    + camera.camera.ScreenToWorldPoint(click)
                    - camera.camera.ScreenToWorldPoint(Input.mousePosition));
        if (Input.GetMouseButtonUp(MouseButtons.Right)
        && !Input.GetMouseButtonDown(MouseButtons.Right))
            camera.controller = new CameraController(camera);
    }
}
