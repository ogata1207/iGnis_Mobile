using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PauseMenu : MonoBehaviour {
    public Image image;

	// Use this for initialization
	void Start () {
        image.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenGUI()
    {
        image.enabled = !image.enabled;
        Time.timeScale = 1 - Time.timeScale;
    }
}
