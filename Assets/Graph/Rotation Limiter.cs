using UnityEngine;

public class RotationLimiter : MonoBehaviour
{
    [Header("X Axis Limits (Up / Down Tilt)")]
    [SerializeField] private float minX = -45f;
    [SerializeField] private float maxX = 45f;

    [Header("Y Axis Limits (Left / Right Turn)")]
    [SerializeField] private float minY = -180f;
    [SerializeField] private float maxY = 180f;

    void LateUpdate()
    {
        Vector3 angles = transform.localEulerAngles;

        // Convert 0-360 angles to -180 to 180
        float x = angles.x;
        float y = angles.y;

        if (x > 180f) x -= 360f;
        if (y > 180f) y -= 360f;

        // Clamp both axes
        x = Mathf.Clamp(x, minX, maxX);
        y = Mathf.Clamp(y, minY, maxY);

        // Apply the clamped rotation
        transform.localEulerAngles = new Vector3(x, y, angles.z);
    }
}