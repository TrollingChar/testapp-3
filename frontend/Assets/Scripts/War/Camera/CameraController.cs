using UnityEngine;

public class CameraController {
    protected CameraWrapper camera;

    public CameraController (CameraWrapper cameraWrapper) {
        this.camera = cameraWrapper;
    }

    virtual public void Update () {
        //if (Input.GetKeyDown(KeyCode.Period)
        //&& !Input.GetKeyDown(KeyCode.Period)) {
        if (Input.GetMouseButtonDown(MouseButtons.Right)
        && !Input.GetMouseButtonUp(MouseButtons.Right)) {
            camera.controller = new MouseBasedCameraController(camera);
        }
    }
}