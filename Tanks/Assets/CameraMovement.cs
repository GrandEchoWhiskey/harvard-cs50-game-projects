using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject player;
    public float sensitivity=2f;
    public bool invertedX = false;
    public bool invertedY = false;
    public float lookXLimit = 45f;

    void Update()
    {
        float rotateHorizontal = Input.GetAxis("Mouse X") * (invertedX ? 1f: -1f) * sensitivity;
        float rotateVertical = Input.GetAxis("Mouse Y") * (invertedY ? 1f : -1f) * sensitivity;
        rotateVertical = Mathf.Clamp(rotateVertical, -lookXLimit, lookXLimit);
        transform.RotateAround(player.transform.position, -Vector3.up, rotateHorizontal);
        transform.RotateAround(Vector3.zero, transform.right, rotateVertical);
    }

}
