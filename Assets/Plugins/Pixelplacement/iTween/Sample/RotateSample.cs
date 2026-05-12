using UnityEngine;
using System.Collections;

public class RotateSample : MonoBehaviour
{	
	void Start(){
		iTween.RotateBy(gameObject, iTween.Hash("x", 1.0, "easeType", "easeInOutBack", "loopType", "pingPong", "delay", .4));
	}
}

