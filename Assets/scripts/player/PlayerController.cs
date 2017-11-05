using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject playerModel;
    public Transform camera;
    public float rightMultiplier = 1f;
    public float forwardMultiplier = 1f;
    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    public float jumpSpeed = 2f;

    public float cameraX = 1f;
    public float cameraY = 1f;
    public float cameraSensitivity = 25f;

    private MagBootsCtrl magBoots;
    private Rigidbody rb;
    private bool _jumping = false;
    private bool _magBoots = false;
    private Vector3 baseGravoty;

	// Use this for initialization
	void Start () {
        playerModel = gameObject;
        camera = transform.Find("CameraHolder");
        rb = GetComponent<Rigidbody>();
        magBoots = transform.FindChild("MagBoots").gameObject.GetComponent<MagBootsCtrl>();
	}
	
	// Update is called once per frame
	void Update () {
        //rb.angularVelocity = new Vector3(0, 0, 0);
        MoveCharacter();
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


        //Debug.Log("v: " + rb.velocity);
        if (Input.GetAxis("Jump") != 0 && IsGrounded())
        {
            Vector3 jump = rb.velocity;
            jump.y = jumpSpeed;
            rb.velocity = jump;
        }
	}

    public bool IsGrounded()
    {
        bool isGrounded = Mathf.Abs(rb.velocity.y) < .00001f;
        //Debug.Log("IsGrounded() == " + isGrounded);
        return isGrounded;
    }
    
    private void ActivateHand(Transform handTrans) {
        if (handTrans != null)
        {
            HandMovement hand = handTrans.gameObject.GetComponent<HandMovement>();
            hand.Swing();
        }
    }

    private void MoveCharacter()
    {
        float right = Input.GetAxis("Horizontal") * rightMultiplier;
        float forward = Input.GetAxis("Vertical") * forwardMultiplier;
        Vector3 move = new Vector3(right, 0, forward) * Time.deltaTime * walkSpeed;
        //playerModel.GetComponent<Rigidbody>().AddForce(move);
        playerModel.transform.Translate(move);
    }

    private void MoveCamera()
    {
        float rotY = Input.GetAxis("Mouse X") * cameraX* Time.deltaTime * cameraSensitivity;
        float rotX = Input.GetAxis("Mouse Y") * cameraY* Time.deltaTime * cameraSensitivity;
        if (rb.useGravity || magBoots.bootsAttached) {
            playerModel.transform.Rotate(new Vector3(0, rotY, 0));
            camera.Rotate(new Vector3(-rotX, 0, 0));
        } else {
            playerModel.transform.Rotate(new Vector3(-rotX, rotY, 0));
        }

    }
}
