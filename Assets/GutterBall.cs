﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GutterBall : MonoBehaviour {

    public bool ballLeftBox;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerExit(Collider collider)
    {
        ballLeftBox = true;
    }
}
