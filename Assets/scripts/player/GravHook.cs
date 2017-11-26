using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravHook : MonoBehaviour {

    public float grappleForce = 5.0f;
    public float grappleRange = 0f;
    public float maxGrappleSpeed = 10.0f;
    public KeyCode grappleKye = KeyCode.G;
    public Color errorMessage = new Color(.5f, .1f, .1f);
    public PlayerController player;

    private RaycastScanner rayScan;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        if (rayScan == null) { rayScan = GetComponent<RaycastScanner>(); }
        rb = player.gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(1))
        {
            Debug.Log("grappling!");
            GameObject target;
            if (grappleRange == 0) {
                target = rayScan.GetVisibleObject();
            } else {
                target = rayScan.GetVisibleObjectWithinDistance(grappleRange);
            }
            if (target == null)
            {
                NotificationManager.inst.PostNotification("grapple", "Nothing to grapple!", errorMessage, 3.0f);
            }
            else if (target.tag == "Grapple")
            {
                Rigidbody rb = target.GetComponent<Rigidbody>();
                if (rb == null) { KinematicGrapple(target); }
                else { ForceGrapple(target);}
            }
            else
            {
                NotificationManager.inst.PostNotification("grapple", "Not a valid grapple point!", errorMessage, 3.0f);
            }
        }
	}
    private void PullTarget(GameObject target)
    {
        Debug.Log("Pull Target");
        Rigidbody target_rb = target.GetComponent<Rigidbody>();
        Vector3 grappleVector = grappleForce * GetVecterToward(target.transform.position);
        target_rb.AddForce(-grappleVector);
        RestrictVelocity(target_rb, maxGrappleSpeed);
    }

    private void KinematicGrapple(GameObject target)
    {
        Debug.Log("Kinematic Grapple");
        Vector3 grappleVector = grappleForce * GetVecterToward(target.transform.position);
        this.rb.AddForce(grappleVector);
        RestrictVelocity(this.rb, maxGrappleSpeed);
    }

    private void ForceGrapple(GameObject target)
    {
        Debug.Log("Force Grapple");
        Rigidbody target_rb = target.GetComponent<Rigidbody>();
        Vector3 grappleVector = grappleForce * GetVecterToward(target.transform.position);

        this.rb.AddForce(grappleVector);
        target_rb.AddForce(-grappleVector);

        RestrictVelocity(this.rb, maxGrappleSpeed);
        RestrictVelocity(target_rb, maxGrappleSpeed);
    }

    // if the given Rigidbody (RB) has a velocity magnitude greater the given maxVelocity, than this method 
    // will reduce the RB's velocity magnitude to the maxVelocity without altering the direction of the vector.
    private static void RestrictVelocity(Rigidbody rb, float maxVelocity) 
    {
        if (rb.velocity.magnitude > maxVelocity)
        {
            Vector3 newV = Vector3.Normalize(rb.velocity) * maxVelocity;
            rb.velocity = newV;
        }
    }

    private Vector3 GetVecterToward(Vector3 target)
    {
        float x = target.x - transform.position.x;
        float y = target.y - transform.position.y;
        float z = target.z - transform.position.z;
        Vector3 v = Vector3.Normalize(new Vector3(x, y, z));
        return v;
    }
}
