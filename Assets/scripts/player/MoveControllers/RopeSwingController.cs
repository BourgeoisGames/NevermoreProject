using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSwingController : MonoBehaviour, IPlayerMovementMode
{
    public GameObject playerModel;

    public PlayerMoveCtrl masterMoveCtrl;

    public float swingForce = 10.0f;

    private GameObject ropeGrabbed;
    private bool _seizeControl = false;
    private bool _releaseControl = false;

	// Use this for initialization
	void Start () {
        if (playerModel == null)
        {
            playerModel = gameObject;
        }
        if (masterMoveCtrl == null)
        {
            masterMoveCtrl = GetComponent<PlayerMoveCtrl>();
        }
	}


    public void Control()
    {
        float multiplier = 0;
        if (Input.GetAxis("Vertical") > 0)
        {
            multiplier = 1;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            multiplier = -1;
        }
        multiplier *= Time.deltaTime * swingForce;
        Vector3 force = multiplier * playerModel.transform.forward;
        playerModel.GetComponent<Rigidbody>().AddForce(force);
    }

    public bool ShouldTakeControl()
    {
        if (_seizeControl)
        {
            _seizeControl = false;
            return true;
        }
        return false;
    }

    public bool ShouldLoseControl()
    {
        if (_releaseControl)
        {
            _releaseControl = false;
            return true;
        }
        return false;
    }

    public void SeizeControl()
    {
        _seizeControl = true;
    }

    public void InitializeControl()
    {
        playerModel.transform.SetParent(ropeGrabbed.transform);
    }

    public void TearDownControl()
    {
        playerModel.transform.SetParent(null);
    }

    public void ReleaseControl()
    {
        _releaseControl = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter on: " + other.gameObject.tag);
        ropeGrabbed = other.gameObject;
        SeizeControl();
    }
}
