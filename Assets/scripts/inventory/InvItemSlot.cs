using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvItemSlot : MonoBehaviour, IDropHandler {

    public IInvUI ui;
    public int index;

    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData) {
        if (!item)
        {
            DragableItem.beingDragged.transform.SetParent(transform);
        }
        if (ui == null) { return; }
        ui.SlotChangedState(this, this.index);
    }

    public void SubscribeTo(IInvUI ui, int index)
    {
        this.ui = ui;
        this.index = index;
    }

}
