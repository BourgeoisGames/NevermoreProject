using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragableItem : MonoBehaviour, IInventoryItem {

    public static GameObject beingDragged;
    public bool selected = false;

    public Color selectedColor = new Color(0.9f, 0.9f, 0.9f);
    public Color notSelectedColor = new Color(1.0f, 1.0f, 1.0f);

    private Transform _originalParent;
    // private Vector3 _startPosition;
    private GameObject _tempItemHolder;

    void Start()
    {
        _tempItemHolder = GameObject.Find("TempItemHolder");
    }

    void OnMouseUp()
    {
        Debug.Log("On Mouse Up");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("On Begin Drag");
        beingDragged = gameObject;
        //_startPosition = transform.position;
        _originalParent = transform.parent;
        transform.SetParent(_tempItemHolder.transform);
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        transform.position = Input.mousePosition;
        Debug.Log("Picked-up item child of:" + transform.parent.gameObject.name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragged Item child of:" + transform.parent.gameObject.name);
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Dropped item child of:" + transform.parent.gameObject.name);
        if (beingDragged.transform.parent == _tempItemHolder.transform)
        {
            transform.SetParent(_originalParent);
        }
        beingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
