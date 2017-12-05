using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkController : MonoBehaviour, IPlayerMovementMode {

    public GameObject playerModel;

    public float rightMultiplier = 1f;
    public float forwardMultiplier = 1f;
    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    public float jumpSpeed = 5f;

    public PlayerMoveCtrl masterMoveCtrl;

    private bool _seizeControl = true;

    public void SeizeControl()
    {
        _seizeControl = true;
    }

    void Start() {
        if (playerModel == null)
        {
            playerModel = gameObject;
        }
        if (masterMoveCtrl == null)
        {
            masterMoveCtrl = GetComponent<PlayerMoveCtrl>();
        }
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
        return false;
    }

	// Update is called once per frame
	public void Control() {
        if (!IsGrounded())
        {
            return;
        }
        
        float right = Input.GetAxis("Horizontal") * rightMultiplier;
        float forward = Input.GetAxis("Vertical") * forwardMultiplier;
        Vector3 move = new Vector3(right, 0, forward) * Time.deltaTime * walkSpeed;
        //playerModel.GetComponent<Rigidbody>().AddForce(move);
        playerModel.transform.Translate(move);
	}

    private void Jump()
    {
        // TODO
    }

    public bool IsGrounded()
    {
        return true;
    }

    public void InitializeControl()
    {
        Vector3 rot = playerModel.transform.rotation.eulerAngles;
        rot = new Vector3(0, rot.y, 0);
        
    }

    public void TearDownControl()
    {
        // do nothing
    }

    public void ReleaseControl()
    {
        // do nothing
    }
}
