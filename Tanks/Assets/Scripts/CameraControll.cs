using UnityEngine;

public class CameraControll : MonoBehaviour
{

    public Camera fpp;
    public Camera tpp;
    public GameObject turret;
    public float sesitivity = 10f;
    public float distance = 10f;

    private const float sesitivityMultipler = 10f;
    private const float yOffset = 1f;
    private float yaw;
    private float pitch;
    private bool firstPerson;

    void Start()
    {

    }

    void Update()
    {
        yaw += Input.GetAxis("Mouse X") * sesitivity * sesitivityMultipler * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * sesitivity * sesitivityMultipler * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            firstPerson = !firstPerson;
        }

        pitch = Mathf.Clamp(pitch, firstPerson ? -30f : 0f, firstPerson ? 30f : 40f);

        tpp.enabled = !firstPerson;
        fpp.enabled = firstPerson;

        FirstPersonPerspective();
        ThirdPersonPerspective();
        CastRay();
    }

    void ThirdPersonPerspective()
    {
        tpp.transform.position = transform.position + Quaternion.Euler(pitch, yaw, 0f) * (-Vector3.forward * distance);
        tpp.transform.LookAt(transform);
    }

    void FirstPersonPerspective()
    {
        fpp.transform.position = new Vector3(turret.transform.position.x, turret.transform.position.y + yOffset, turret.transform.position.z);
        fpp.transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }

    void CastRay()
    {
        Vector3 origin = transform.position;
        Vector3 direction = fpp.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, fpp.nearClipPlane)) - origin;
        Ray ray = new Ray(origin, direction);
        Debug.DrawRay(origin, direction, Color.red);
    }

}

