﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_TELEPORT : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = Vector3.zero;
        }
	}
}
