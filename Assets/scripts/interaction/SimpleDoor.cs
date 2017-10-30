using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoor : MonoBehaviour, Interactive {

	public float openDuration = 10f;
    private float _closeIn = 0;
	
	// Update is called once per frame
	void Update () {
        Debug.Log("_closeIn: " + _closeIn);
        if (_closeIn > 0)
        {
            _closeIn -= Time.deltaTime;
            if (_closeIn <= 0)
            {
                SetChildrenActive(true, transform);
            }
        }
	}

    public void Interact()
    {
        SetChildrenActive(false, transform);
        _closeIn = openDuration;
    }

    public void SetChildrenActive(bool active, Transform trans)
    {
        foreach (Transform child in trans)
        {
            SetChildrenActive(active, child);
            child.gameObject.SetActive(active);
        }
    }
}
