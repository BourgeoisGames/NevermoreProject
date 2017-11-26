using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIOpener : MonoBehaviour {

    public KeyCode inventoryKey = KeyCode.I;
    public PlayerController playerScript;
    public GameObject invPanel;

	// Use this for initialization
	void Start () {
        if (playerScript == null)
        {
            GameObject player = GameObject.Find("PlayerCharacter");
            if (player != null) {
                playerScript = player.GetComponent<PlayerController>();
            }
        }
        if (invPanel == null)
        {
            invPanel = GameObject.Find("InventoryPanel");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(inventoryKey))
        {
            ToggleInventory();
        }
	}

    public void ToggleInventory()
    {
        if (invPanel.activeInHierarchy)
        {
            ToggleInventoryOff();
        }
        else
        {
            ToggleInventoryOn();
        }
    }

    public void ToggleInventoryOn() 
    {
        invPanel.SetActive(true);
        playerScript.locked = true;
    }

    public void ToggleInventoryOff()
    {
        GameObject canvas = GameObject.Find("Canvas");
        foreach (Transform child in canvas.transform)
        {
            if (child.tag == "InventoryUI")
            {
                child.gameObject.SetActive(false);
            }
        }
        playerScript.locked = false;
    }

    public void OPEN_ALL_INVENTORY()
    {
        Debug.LogWarning("Using OPEN_ALL_INVENTORY intended only for DEBUGING!!!");
        GameObject canvas = GameObject.Find("Canvas");
        foreach (Transform child in canvas.transform)
        {
            if (child.tag == "InventoryUI")
            {
                child.gameObject.SetActive(true);
            }
        }
        playerScript.locked = true;
    }
}
