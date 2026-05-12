using UnityEngine;

public class MoveModel : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.01f;
    [SerializeField] private float minY = -2f;
    [SerializeField] private float maxY = 2f;

    void Update()
    {
        // Hold right mouse button and drag up/down
        if (Input.GetMouseButton(1))
        {
            float mouseY = Input.GetAxis("Mouse Y");

            // Move the object up and down
            transform.position += Vector3.up * mouseY * moveSpeed;

            // Clamp the Y position
            Vector3 pos = transform.position;
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            transform.position = pos;
        }
    }
}