using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRigidBody : MonoBehaviour
{
    public float force = 1.0f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        rb.AddForce(0, 0, force * Time.deltaTime, ForceMode.Impulse);
    }
}
