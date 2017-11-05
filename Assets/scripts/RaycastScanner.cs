using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RaycastScanner : MonoBehaviour {

    public enum CastDirection
    {
        up, down, left, right, forward, backward
    }

    public CastDirection direction = CastDirection.forward;

    public RaycastHit GetRaycastHit()
    {
        Vector3 castDirection = GetCastDirection();
        RaycastHit objHit;
        bool hit = Physics.Raycast(transform.position, castDirection, out objHit);
        return objHit;
    }

    private Vector3 GetCastDirection()
    {
        if (direction == CastDirection.forward) { return transform.forward; }
        else if (direction == CastDirection.backward) { return -transform.forward; }
        else if (direction == CastDirection.right) { return transform.right; }
        else if (direction == CastDirection.left) { return -transform.right; }
        else if (direction == CastDirection.up) { return transform.up; }
        else if (direction == CastDirection.down) { return -transform.up; }

        // this code should never execute, if it does, someting is rong.
        Debug.Log("WARNING - RaycastScanner.GetCastDirection() reached ELSE case.");
        return transform.forward; // default case
    }

    public GameObject GetVisibleObject()
    {
        RaycastHit objHit = GetRaycastHit();
        if (objHit.transform == null) { return null; }
        return objHit.transform.gameObject;
    }

    public GameObject GetVisibleObjectWithinDistance(float maxDistance)
    {
        RaycastHit objHit = GetRaycastHit();
        if (objHit.transform == null) { return null; }
        if (objHit.distance > maxDistance) { return null; }
        return objHit.transform.gameObject;
    }
}
