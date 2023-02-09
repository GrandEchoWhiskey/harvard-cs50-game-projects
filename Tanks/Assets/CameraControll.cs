using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraControll : MonoBehaviour
{

    public Camera defaultCamera;
    public Camera additionalCamera;

    void Start()
    {
        defaultCamera.enabled = true;
        additionalCamera.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) SwapCam(defaultCamera, additionalCamera);
    }

    void SwapCam(Camera cam1, Camera cam2)
    {
        cam1.enabled = !cam1.enabled;
        cam2.enabled = !cam1.enabled;
    }

}
