using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithParent : MonoBehaviour, Interactive {

    public void Interact()
    {
        Interactive parent = transform.parent.gameObject.GetComponent<Interactive>();
        if (parent != null)
        {
            parent.Interact();
        }
    }
}
