using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCtrl : MonoBehaviour {

    public GameObject playerModel;
    public Transform camera;
/*
    public float rightMultiplier = 1f;
    public float forwardMultiplier = 1f;
    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    public float jumpSpeed = 5f;
//*/

    public float cameraX = 1f;
    public float cameraY = 1f;
    public float cameraSensitivity = 25f;

    public bool locked = false;

    private Rigidbody rb;

    public IPlayerMovementMode defaultMode;
    private IPlayerMovementMode _currentMode;
    
    IPlayerMovementMode currentMode{
        get
        {
            return _currentMode;
        }
        set
        {
            if (_currentMode != null)
            {
                _currentMode.TearDownControl();
            }
            _currentMode = value;
            if (_currentMode != null)
            {
                _currentMode.InitializeControl();
            }
        }
    }
    List<IPlayerMovementMode> movementModes;

	// Use this for initialization
	void Start () {
        playerModel = gameObject;
        camera = transform.Find("CameraHolder");
        rb = GetComponent<Rigidbody>();

        movementModes = new List<IPlayerMovementMode>();
        WalkController walk = GetComponent<WalkController>();
        movementModes.Add(walk);
        currentMode = movementModes[0];
        defaultMode = movementModes[0];

        movementModes.Add(GetComponent<RopeSwingController>());
        

	}
	
	// Update is called once per frame
	void Update () {
        if (locked) { return; }
        //rb.angularVelocity = new Vector3(0, 0, 0);
        //MoveCharacter();
        CheckForNewController();
        currentMode.Control();
        MoveCamera();
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("main fire!");
            Transform hand = camera.Find("RightHand");
            ActivateHand(hand);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("alt fire!");
            Transform hand = camera.Find("LeftHand");
            ActivateHand(hand);
        }
	}

    private void CheckForNewController()
    {
        if (currentMode != null)
        {
            if (currentMode.ShouldLoseControl()) { currentMode = null; }
        }
        foreach (IPlayerMovementMode mode in this.movementModes)
        {
            if (mode.ShouldTakeControl())
            {
                currentMode = mode;
                break;
            }
        }
        if (currentMode == null) { currentMode = defaultMode; }
    }

    private void EndControl()
    {
        currentMode.TearDownControl();
        currentMode = null;
    }

    

    /*
    void FixedUpdate()
    {
        if (Input.GetAxis("Jump") != 0 && IsGrounded())
        {
            Debug.Log("jumping!");
            Vector3 jump = rb.velocity + (transform.up * jumpSpeed);
            rb.velocity = jump;
        }
    }
    //* /
    public bool IsGrounded()
    {
        bool isGrounded = Mathf.Abs(rb.velocity.y) < .00001f;
        //Debug.Log("IsGrounded() == " + isGrounded);
        return isGrounded;
    }
    //* /

    private void MoveCharacter()
    {
        float right = Input.GetAxis("Horizontal") * rightMultiplier;
        float forward = Input.GetAxis("Vertical") * forwardMultiplier;
        Vector3 move = new Vector3(right, 0, forward) * Time.deltaTime * walkSpeed;
        //playerModel.GetComponent<Rigidbody>().AddForce(move);
        playerModel.transform.Translate(move);
    }

    //*/
    
    private void ActivateHand(Transform handTrans) {
        if (handTrans != null)
        {
            HandMovement hand = handTrans.gameObject.GetComponent<HandMovement>();
            if (hand != null)
            {
                hand.Swing();
            }
        }
    }

    public bool IsGrounded()
    {
        return true;
    }

    private void MoveCamera()
    {
        float rotY = Input.GetAxis("Mouse X") * cameraX* Time.deltaTime * cameraSensitivity;
        float rotX = Input.GetAxis("Mouse Y") * cameraY* Time.deltaTime * cameraSensitivity;
        if (rb.useGravity) { // || magBoots.bootsAttached) {
            playerModel.transform.Rotate(new Vector3(0, rotY, 0));
            camera.Rotate(new Vector3(-rotX, 0, 0));
        } 
//        else {
//            playerModel.transform.Rotate(new Vector3(-rotX, rotY, 0));
//        }

    }
}
