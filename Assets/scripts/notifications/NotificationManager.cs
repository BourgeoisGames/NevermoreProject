using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour {

    // TODO eventually, flashing text can be implemented in a script added to the Text GameObject at creation.

    public GameObject canvas, notificationArea;
    public float update_threshold = 1f;
    public int fontSize = 18;

    private static NotificationManager _instance;
    public static NotificationManager inst
    {
        get { return _instance; }
    }

    private Dictionary<string, Notification> notifications = new Dictionary<string,Notification>();
    private float since_last_update;

    void Awake()
    {
        _instance = this;
        Notification.defaultFontSize = fontSize;
        since_last_update = 0;
        
        if (canvas == null) {
            canvas = GameObject.Find("Canvas");
        }
        if (notificationArea == null)
        {
            notificationArea = canvas.transform.Find("Notifications").gameObject;
        }

        foreach (Transform child in notificationArea.transform) 
        {
            // Debug.Log("removing a child from notification area at startup.");
            Destroy(child.gameObject);
        }
    }
	
	void Update () {
        since_last_update += Time.deltaTime;
        if (since_last_update < update_threshold) {
            // only update once every [update_threshold] seconds.
            return;
        }
        UpdateNotifications();
        RemoveExpiredNotifications();
	}

    public void UpdateNotifications()
    {
        foreach (string key in notifications.Keys)
        {
            Notification value = notifications[key];
            if (value.durationRemaining == float.MaxValue)
                continue;
            value.durationRemaining -= since_last_update;
        }
        since_last_update = 0;
    }

    public void RemoveExpiredNotifications()
    {
        List<string> toRemove = new List<string>();
        foreach (string key in notifications.Keys)
        {
            Notification value = notifications[key];
            if (value.durationRemaining <= 0)
            {
                toRemove.Add(key);
            }
        }
        foreach (string key in toRemove)
        {
            // Debug.Log("removing 1 expired notification!");
            Destroy(notifications[key]._textObj);
            notifications.Remove(key);
        }
    }

    public void PostIndefiniteNotification(string key, string text, Color color)
    {
        PostNotification(key, text, color, float.MaxValue);
    }

    public void ResetNotificationDuration(string key, float newDuration)
    {
        UpdateNotifications();
        Notification note = notifications[key];
        note.durationRemaining = newDuration;
        RemoveExpiredNotifications();
        // Debug.Log("notification '" + key + "' duration set to " + newDuration);
    }

    public void PostNotification(string key, string text, Color color, float duration)
    {
        if (notifications.ContainsKey(key))
        {
            ReplaceNotification(key, text, color, duration);
        }
        else
        {
            PostNewNotification(key, text, color, duration);
        }
    }

    private void ReplaceNotification(string key, string text, Color color, float duration)
    {
        Notification note = notifications[key];
        note.color = color;
        note.text = text;
        note.durationRemaining = duration;
    }

    private void PostNewNotification(string key, string text, Color color, float duration)
    {
        // Debug.Log("posting new notification - " + key + ": " + text + ". Duration: " + duration);
        UpdateNotifications();
        RemoveExpiredNotifications();
        Notification note = new Notification(notificationArea.transform);
        note.text = text;
        note.color = color;
        note.durationRemaining = duration;
        notifications.Add(key, note);
    }

    public bool RemoveNotification(string key)
    {
        if (!notifications.ContainsKey(key)) {
            Debug.Log("failed to remove notification '" + key + "' because it was not found!");
            return false;
        }
        Notification note = notifications[key];
        Destroy(note._textObj); // This is not ideal...
        notifications.Remove(key);
        Debug.Log("successfully removed notification '" + key + "'");
        return true;
    }

    //*
    private class Notification
    {
        public static int defaultFontSize = 18;
        public static Font defaultFont = new Font("Arial");
        
        public GameObject _textObj;
        public string text
        {
            get { return _textObj.GetComponent<Text>().text; }
            set { _textObj.GetComponent<Text>().text = value; }
        }

        public Color color
        {
            get { return _textObj.GetComponent<Text>().color; }
            set { _textObj.GetComponent<Text>().color = value; }
        }

        public float durationRemaining;

        public Notification(Transform parent)
        {
            _textObj = MakeNewText("notification");
            _textObj.transform.SetParent(parent);
            //Debug.Log("created new notification");
        }

        private GameObject MakeNewText(string objName)
        {
            GameObject textObj = Instantiate<GameObject>(Resources.Load<GameObject>("PrefabText"));
            /* GameObject textObj = new GameObject(objName);
            Text text = textObj.AddComponent<Text>();
            text.fontSize = defaultFontSize;
            text.font = defaultFont;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.verticalOverflow = VerticalWrapMode.Overflow; */
            return textObj;
        }
    }
    //*/
}
