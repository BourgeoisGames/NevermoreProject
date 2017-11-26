using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelInteraction : MonoBehaviour, Interactive {

    public string notificationGroup = "End Level";
    public string cannotLeaveMessage = "You have Not Finished your mission!";
    public Color notifColor = new Color(.7f, .1f, .1f);
    public float notifDuration = 5.0f;

    public void Interact()
    {
        NotificationManager.inst.PostNotification("EndLevel", cannotLeaveMessage, notifColor, notifDuration);
    }
}
