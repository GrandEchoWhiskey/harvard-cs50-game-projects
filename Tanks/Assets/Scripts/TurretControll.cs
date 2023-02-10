using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControll : MonoBehaviour
{
    public GameObject turret;
    public float rotationSpeed = 10f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Ray cameraRay = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(cameraRay, out hit))
        {
            Vector3 lookAtTarget = new Vector3(hit.point.x, turret.transform.position.y, hit.point.z);
            Quaternion rotation = Quaternion.LookRotation(lookAtTarget - turret.transform.position);
            turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}




