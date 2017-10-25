using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNotificationInteraction : MonoBehaviour {

    bool test = true;
	
	// Update is called once per frame
	void Update () {
        if (test)
        {
            Interactive interactive = GetComponent<Interactive>();
            interactive.Interact();
            test = false;
        }
	}
}
