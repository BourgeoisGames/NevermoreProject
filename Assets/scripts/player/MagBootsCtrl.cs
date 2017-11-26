using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagBootsCtrl : MonoBehaviour {

    public Rigidbody rb;
    public KeyCode toggleKey;
    
    public float downForce = 9.81f;

    // furthest distance below the character which ground will be detected.
    public float maxDistToGround = 1.0f;

    public string notifKey = "mag boots";

    private RaycastScanner rayScan;

    // Use this for initialization
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (rb == null)
        {
            rb = transform.parent.gameObject.GetComponent<Rigidbody>();
        }

        rayScan = GetComponent<RaycastScanner>();
        if (rayScan == null)
        {
            rayScan = gameObject.AddComponent<RaycastScanner>();
        }
        rayScan.direction = RaycastScanner.CastDirection.down;
    }

    private bool _bootsAttached;
    public bool bootsAttached
    {
        get { return _bootsAttached; }
    }

    private bool AttachBoots()
    {
        GameObject ground = this.rayScan.GetVisibleObjectWithinDistance(maxDistToGround);
        if (ground == null) { 
            _bootsAttached = false;
            string msg = "failed to attach mag boots";
            Color msgColor = new Color(.1f, .1f, .1f);
            NotificationManager.inst.PostNotification(notifKey, msg, msgColor, 3.0f);
            return false;
        }
        AlignWithObject(ground.transform);
        _bootsAttached = true;
        return true;
    }

    private bool DetachBoots()
    {
        _bootsAttached = false;
        return false;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (bootsAttached)
            {
                DetachBoots();
            }
            else
            {
                AttachBoots();
            }
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (_bootsAttached)
        {
            Debug.Log("magboots attached");
            MitigateGravity();
            ApplyBootDownForce();
        }
	}

    private void AlignWithObject(Transform trans)
    {
        this.transform.parent.rotation = trans.rotation;
    }

    private void ApplyBootDownForce()
    {
        Vector3 force = -transform.up * downForce;
        rb.AddForce(force);
    }

    private void MitigateGravity()
    {
        if (!rb.useGravity) { return; }
        Debug.Log("mitigating gravity");
        Vector3 antiGrav = -Physics.gravity;
        rb.AddForce(antiGrav);
    }
}
