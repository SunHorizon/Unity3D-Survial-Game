﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsLimit : MonoBehaviour {

	// Use this for initialization
	void Start () {

        QualitySettings.vSyncCount = 0; 
        Application.targetFrameRate = 60;
    }

}
