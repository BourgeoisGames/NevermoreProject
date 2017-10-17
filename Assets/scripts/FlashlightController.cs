using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour {

    public bool flashlightEnabled = true;
    public KeyCode toggleKey;

    private Light flashlight;

	// Use this for initialization
	void Start () {
        flashlight = GetComponent<Light>();
        flashlight.enabled = flashlightEnabled;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(toggleKey))
        {
            flashlightEnabled = !flashlightEnabled;
            flashlight.enabled = flashlightEnabled;
        }
	}
}
