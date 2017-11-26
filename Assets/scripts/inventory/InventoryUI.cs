using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IInvUI, Interactive {

    public int width = 4;
    public int height = 2;
    public float cellSize = 110.0f;

    public GameObject root;
    public bool startVisible;

    private GridLayoutGroup grid;
    private GameObject[] itemSlots;

	// Use this for initialization
	void Start () {
        if (root == null)
        {
            root = transform.parent.gameObject;
        }

        grid = gameObject.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(cellSize, cellSize);
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = width;
        grid.childAlignment = TextAnchor.MiddleCenter;

        itemSlots = new GameObject[width * height];
        GameObject prefab = Resources.Load<GameObject>("ItemSlot");
        for (int i = 0; i < width * height; i++)
        {
            Debug.Log("generating stuff " + i);
            GameObject slot = Instantiate<GameObject>(prefab);
            slot.name = "Item Slot " + (i+1);
            slot.transform.SetParent(transform);
            itemSlots[i] = slot;

            InvItemSlot slotScript = slot.GetComponent<InvItemSlot>();
            slotScript.SubscribeTo(this, i);
        }
        prefab = Resources.Load<GameObject>("SampleItem");
        GameObject item = Instantiate<GameObject>(prefab);
        item.transform.SetParent(itemSlots[1].transform);

        root.SetActive(startVisible);
	}

    public void OpenInventory()
    {
        root.SetActive(true); 
        GameObject uiCtrl = GameObject.Find("UI-Controller");
        UIOpener uiCtrlScript = uiCtrl.GetComponent<UIOpener>();
        uiCtrlScript.ToggleInventoryOn(); 
    }

    public void SlotChangedState(InvItemSlot slot, int index)
    {
        Debug.Log("slot " + index + " has a new item.");
    }

    public void Interact()
    {
        OpenInventory();
    }
}
