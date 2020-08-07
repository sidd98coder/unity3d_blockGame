using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{

    public float targetDistance;
    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hitObject;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitObject))
        {
            targetDistance = hitObject.distance;
        }
    }
}
