using UnityEngine;
using System.Collections;

public class RotateVert : MonoBehaviour
{
    void Start()
    {
        iTween.RotateBy(gameObject, iTween.Hash("y", 1.0, "easeType", "easeInOutBack", "loopType", "pingPong", "delay", .4));
    }
}

