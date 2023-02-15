using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour
{
    public GravityOrbit Gravity;
    private Rigidbody rb;

    public float RotationSpeed = 20;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

   
    void FixedUpdate()
    {
        if (Gravity)
        {
            Vector3 gravityUp = Vector3.zero;

            gravityUp = Gravity.transform.up;

            Vector3 LocalUp = transform.up;

            Quaternion targetRotation = Quaternion.FromToRotation(LocalUp, gravityUp) * transform.rotation;

            transform.up = Vector3.Lerp(transform.up, gravityUp, RotationSpeed * Time.deltaTime);

            //push down for gravity
            rb.AddForce((-gravityUp * Gravity.Gravity) * rb.mass);
        }
    }
}
