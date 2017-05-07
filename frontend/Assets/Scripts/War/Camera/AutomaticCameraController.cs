using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AutomaticCameraController : CameraController {

    public AutomaticCameraController (CameraWrapper cameraWrapper) : base(cameraWrapper) { }

    public override void Update () {
        camera.LookAt(new Vector3(1000, 0, 0));
    }
}
