using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBuilder : MonoBehaviour {

    public GameObject ropeSegmentPrefab;
    public GameObject pivot;
    public int segments;
    public float segmentSpacing;

	// Use this for initialization
	void Start () {
        Vector3 increment = new Vector3(0, -segmentSpacing, 0);
        Vector3 point = pivot.transform.position + increment/2;
        GameObject previous = pivot;
        for (int i = 0; i < segments; i++)
        {
            GameObject ropeSection = Instantiate<GameObject>(ropeSegmentPrefab);
            ropeSection.transform.SetParent(pivot.transform.parent);
            ropeSection.transform.position = point;
            if (previous != null)
            {
                CharacterJoint joint = ropeSection.GetComponent<CharacterJoint>();
                Rigidbody previousRb = previous.GetComponent<Rigidbody>();
                joint.connectedBody = previousRb;
            }
            previous = ropeSection;
            point += increment;
        }
	}
}
