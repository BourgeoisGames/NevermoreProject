using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScanner : MonoBehaviour {

    public float interactionDistance = 1f;
    public bool notifyInteraction = true;
    public string interactionMessage = "Click to Interact!";
    public bool showInteractionMessage = true;
    public string notifKey = "reticle";

    private float notifDuration = .01f;
    private Color notifColor = new Color(0, 0, 0);
    private RaycastScanner rayScan;

    void Start()
    {
        rayScan = gameObject.GetComponent<RaycastScanner>();
        if (rayScan == null)
        {
            rayScan = gameObject.AddComponent<RaycastScanner>();
        }
    }

    private void NoticeObject(string text)
    {
        // Debug.Log(text);
        if (notifyInteraction)
        {
            NotificationManager.inst.PostNotification(notifKey, text, notifColor, notifDuration);
        }
    }
	
	// Update is called once per frame
	void Update () {
        GameObject obj = rayScan.GetVisibleObjectWithinDistance(interactionDistance);
        if (CanInteractWith(obj, true))
        {
            if (Input.GetMouseButtonDown(0))
            {
                obj.GetComponent<Interactive>().Interact();
            }
        }
	}

    private bool CanInteractWith(GameObject obj)
    {
        return CanInteractWith(obj, showInteractionMessage);
    }

    private bool CanInteractWith(GameObject obj, bool notify)
    {
        if (obj != null)
        {
            Interactive interaction = obj.GetComponent<Interactive>();
            if (interaction != null)
            {
                if (notify) { NoticeObject(interactionMessage); }
                return true;
            }
        }
        if (notify) {
            Debug.Log("notif key: " + notifKey);
            NotificationManager.inst.RemoveNotification(notifKey);
        }
        return false;
    }
}
