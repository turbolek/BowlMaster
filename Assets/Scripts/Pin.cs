using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pin : MonoBehaviour {

    public float standingTreshold = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
    public bool IsStanding()
    {
        Vector3 rotation = transform.eulerAngles;

        float tiltX = GetTiltFromAngle(rotation.x);
        float tiltZ = GetTiltFromAngle(rotation.z);

        bool result = ((tiltX <= standingTreshold && tiltZ <= standingTreshold));
        return result;
    }

    private float GetTiltFromAngle (float angle)
    {
        float tilt = (angle > 180) ? angle - 360 : angle;
        return Mathf.Abs(tilt);
    }
}
