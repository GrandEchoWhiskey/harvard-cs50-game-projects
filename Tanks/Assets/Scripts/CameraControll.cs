using UnityEngine;

public class CameraControll : MonoBehaviour
{
    [Header("Camera Objects (required)")]
    public Camera fpp;
    public Camera tpp;

    [Header("General")]
    public float sesitivity = 10f;
    public bool firstPerson;

    [Header("TPP Camera")]
    [Range(0f, 6f)]
    public float TopView = 3f;
    public float distance = 10f;

    private float TMinAngle = 0f;
    private float TMaxAngle = 45f;

    private float FMinAngle = -30f;
    private float FMaxAngle = 15f;

    private const float sesitivityMultipler = 10f;
    private const float yOffset = 1f;
    private float yaw;
    private float pitch;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        fpp.transform.position = new Vector3(fpp.transform.position.x, fpp.transform.position.y + yOffset, fpp.transform.position.z);
    }

    void Update()
    {
        CameraSwap(KeyCode.Tab);

        yaw += Input.GetAxis("Mouse X") * sesitivity * sesitivityMultipler * Time.deltaTime;

        pitch -= Input.GetAxis("Mouse Y") * sesitivity * sesitivityMultipler * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, firstPerson ? FMinAngle : TMinAngle, firstPerson ? FMaxAngle : TMaxAngle);

        FirstPersonPerspective();
        ThirdPersonPerspective();
    }

    void CameraSwap(KeyCode code)
    {
        if (Input.GetKeyDown(code)) firstPerson = !firstPerson;

        tpp.enabled = !firstPerson;
        fpp.enabled = firstPerson;
    }

    void ThirdPersonPerspective()
    {
        tpp.transform.position = transform.position + Quaternion.Euler(pitch, yaw, 0f) * (-Vector3.forward * distance);
        tpp.transform.LookAt(new Vector3(transform.position.x, transform.position.y + TopView, transform.position.z));
    }

    void FirstPersonPerspective()
    {
        fpp.transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }

}

