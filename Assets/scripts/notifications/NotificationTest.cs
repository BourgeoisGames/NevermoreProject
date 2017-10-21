using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Color darkColor = new Color(0.2f, 0.2f, 0.2f);
        NotificationManager nMgr = gameObject.GetComponent<NotificationManager>();
        nMgr.PostIndefiniteNotification("indefinite", "this should not go away!", new Color(0.6f, 0.2f, 0.2f));
        nMgr.PostNotification("temporary", "this should go away in 3 seconds!", darkColor, 3.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
