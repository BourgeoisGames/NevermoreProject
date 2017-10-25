using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationInteraction : MonoBehaviour, Interactive {

    public string notificationText = "you touched the but!!";
    public string notificationKey = "you touched the but!!";
    public Color notificationColor = new Color(0.2f, 0.2f, 0.2f);
    public float notificationDuration = 3.0f;

    public void Interact()
    {
        NotificationManager.inst.PostNotification(notificationKey, notificationText, notificationColor, notificationDuration);
    }
}
