using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour, IDamageable {


    public Transform target;
    public float rotationSpeed = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (target != null)
        {
            FaceTarget(target);
        }
	}

    public void FaceTarget(Transform lookTarget)
    {
        //find the vector pointing from our position to the target
        Vector3 direction = (lookTarget.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        float y = Quaternion.LookRotation(direction).eulerAngles.y;
        Quaternion lookRotation = Quaternion.Euler(new Vector3(0, y, 0));

        //rotate us over time according to speed until we are in the required rotation
        float rotSpeed = rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotSpeed);
    }

    public void TakeDamage(float damage)
    {
        // TODO
    }
}
