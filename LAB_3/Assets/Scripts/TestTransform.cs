using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTransform : MonoBehaviour
{
    public float force = 2.0f;
    public Rigidbody rb;

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
        // składowa y wektora prędkości
        if(rb.velocity.y == 0)
        {
            // działamy siłą na ciało A :)
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }
}
