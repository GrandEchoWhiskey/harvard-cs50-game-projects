using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControll : MonoBehaviour
{
    [Header("Turret Rotation")]
    public float rotationSpeed = 3f;
    public bool rotate = true;

    [Header("Muzzle Elevation")]
    public GameObject muzzleObject;
    public float elevationSpeed = 3f;
    public bool elevate = true; 

    [Range(0, 15)] public float maxBotElevation = 10f;
    [Range(0, -30)] public float maxTopElevation = -20f;

    void Update()
    {
        RotateTurret(rotate ? 1f : -1f);
        ElevateMuzzle(elevate ? 1f: -1f);
    }

    void RotateTurret(float amount)
    {
        amount = Mathf.Clamp(amount, -1f, 1f);

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, amount * 7 * rotationSpeed, 0) * Time.fixedDeltaTime);
        transform.rotation *= deltaRotation;
    }

    void ElevateMuzzle(float amount)
    {
        amount = Mathf.Clamp(amount, -1f, 1f);

        float currentPitch = muzzleObject.transform.localEulerAngles.x;
        float moveAmount = amount * elevationSpeed * Time.fixedDeltaTime;
        float newPitch = (currentPitch > 180f ? currentPitch - 360f : currentPitch) + moveAmount;

        newPitch = Mathf.Clamp(newPitch, maxTopElevation, maxBotElevation);

        muzzleObject.transform.localEulerAngles = new Vector3(newPitch < 0 ? newPitch + 360f : newPitch, 0f, 0f);
    }

}