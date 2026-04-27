using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 50f;
    public float minDistance = 10f;
    public float maxDistance = 100f;
    public float rotationSpeed = 5f;
    public float zoomSpeed = 5f;
    public float smoothSpeed = 10f;

    private float yaw = 0f;
    private float pitch = 20f;
    private float currentDistance;

    void Start()
    {
        currentDistance = distance;
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void LateUpdate()
    {
        // Rotate on right mouse button drag
        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * rotationSpeed;
            pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
            pitch = Mathf.Clamp(pitch, -89f, 89f);
        }

        // Also rotate on middle mouse button drag
        if (Input.GetMouseButton(2))
        {
            yaw += Input.GetAxis("Mouse X") * rotationSpeed;
            pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
            pitch = Mathf.Clamp(pitch, -89f, 89f);
        }

        // Zoom with scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed * distance * 0.1f;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        currentDistance = Mathf.Lerp(currentDistance, distance, Time.deltaTime * smoothSpeed);

        Vector3 targetPos = target != null ? target.position : Vector3.zero;

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 position = targetPos - rotation * Vector3.forward * currentDistance;

        transform.rotation = rotation;
        transform.position = position;
    }
}
