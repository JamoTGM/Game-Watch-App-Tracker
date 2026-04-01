using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WheelScroll : MonoBehaviour
{
    public RectTransform content;
    public RectTransform viewport;
    public List<RectTransform> items;

    public float spacing = 150f;
    public float scaleMultiplier = 0.5f;
    public float snapSpeed = 10f;

    private float targetY;

    void UpdateItems()
    {
        float centerY = viewport.position.y;

        foreach (var item in items)
        {
            float distance = item.position.y - centerY;

            float absDistance = Mathf.Abs(distance);

            // Scale effect
            float scale = Mathf.Lerp(1f, scaleMultiplier, absDistance / 300f);
            item.localScale = new Vector3(scale, scale, 1f);

            // Horizontal curve (wheel effect)
            float xOffset = Mathf.Sin(distance / 200f) * 50f;

            item.localPosition = new Vector3(
                xOffset,
                item.localPosition.y,
                0
            );
        }
    }
}