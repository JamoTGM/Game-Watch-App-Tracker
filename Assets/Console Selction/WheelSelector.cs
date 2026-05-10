using UnityEngine;

public class WheelSelector : MonoBehaviour
{
    public float radius = 5f;
    public float rotationSpeed = 100f;

    void Start()
    {
        ArrangeObjects();
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        transform.Rotate(0, scroll * rotationSpeed, 0);
    }

    void ArrangeObjects()
    {
        int count = transform.childCount;

        for (int i = 0; i < count; i++)
        {
            float angle = i * Mathf.PI * 2 / count;

            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            Transform child = transform.GetChild(i);

            child.localPosition = new Vector3(x, 0, z);

            child.LookAt(transform.position);
        }
    }
}