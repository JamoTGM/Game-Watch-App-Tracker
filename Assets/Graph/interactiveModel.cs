using UnityEngine;

public class InteractiveModel : MonoBehaviour
{
    [Header("Auto Spin")]
    [SerializeField] private Vector3 autoRotationSpeed = new Vector3(0f, 10f, 0f);
    [SerializeField] private bool autoSpin = true;

    [Header("User Rotation")]
    [SerializeField] private float dragSensitivity = 0.1f;

    [Header("Zoom")]
    [SerializeField] private Camera targetCamera;
    [SerializeField] private float zoomSpeed = 2.5f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float maxDistance = 15f;

    private bool isDragging = false;

    void Start()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;

        
        float startDistance =
            Vector3.Distance(targetCamera.transform.position, transform.position);

        maxDistance = Mathf.Max(maxDistance, startDistance);
    }

    void Update()
    {
        HandleInput();
        HandleZoom();

        if (autoSpin && !isDragging)
        {
            transform.Rotate(autoRotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
            isDragging = true;

        if (Input.GetMouseButtonUp(0))
            isDragging = false;

        if (isDragging)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            transform.Rotate(Vector3.up, -mouseX * dragSensitivity * 25f, Space.World);
            transform.Rotate(Vector3.right, mouseY * dragSensitivity * 25f, Space.World);
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                isDragging = true;

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                isDragging = false;

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.deltaPosition;

                transform.Rotate(Vector3.up, -delta.x * dragSensitivity, Space.World);
                transform.Rotate(Vector3.right, delta.y * dragSensitivity, Space.World);
            }
        }
    }

    void HandleZoom()
    {
        if (targetCamera == null) return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.001f)
        {
            Vector3 direction = (targetCamera.transform.position - transform.position).normalized;
            float currentDistance = Vector3.Distance(targetCamera.transform.position, transform.position);

            currentDistance -= scroll * zoomSpeed;
            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

            targetCamera.transform.position = transform.position + direction * currentDistance;
        }

    }

    public void SetAutoSpin(bool enabled)
    {
        autoSpin = enabled;
    }

    public void ToggleAutoSpin()
    {
        autoSpin = !autoSpin;
    }
}