using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScanner : MonoBehaviour {

    public float interactionDistance = 1f;
    public bool notifyInteraction = true;

    private string notifKey = "reticle";
    private float notifDuration = .01f;
    private Color notifColor = new Color(0, 0, 0);
    


    private void NoticeObject(string text)
    {
        Debug.Log(text);
        if (notifyInteraction)
        {
            NotificationManager.inst.PostNotification(notifKey, text, notifColor, notifDuration);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        RaycastHit objHit;
        bool hit = Physics.Raycast(transform.position, transform.forward, out objHit);
        if (hit)
        {
            if (objHit.distance > interactionDistance)
            {
                NoticeObject("object is to far away to interact!");
                return;
            }
            Interactive interaction = objHit.transform.gameObject.GetComponent<Interactive>();
            if (interaction == null)
            {
                NoticeObject("object is not interactive!");
            }
            else if (Input.GetMouseButtonDown(0))
            {
                interaction.Interact();
            }
            else
            {
                NoticeObject("Click to interact!");
            }
        }
        else
        {
            Debug.Log("no ray hits!");
            NotificationManager.inst.RemoveNotification(notifKey);
        }
	}
}
