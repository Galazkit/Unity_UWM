using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zad1 : MonoBehaviour
{
    public float Speed = 2f;
    private bool isRunning = false;
    public float distance = 3.0f;
    private bool isAway = false;
    private float backward;
    private float forward;

    void Start()
    {
        forward = transform.position.z + distance;
        backward = transform.position.z;
    }

    void Update()
    {
        if (transform.position.z >= forward && isAway == false)
        {
            isAway = true;
            Speed = -Speed;
        }
        if (transform.position.z < backward && isAway == true)
        {
            isRunning = false;
            isAway = false;
            Speed = -Speed;
        }
        if (isRunning)
        {
            Vector3 move = transform.forward * Speed * Time.deltaTime;
            transform.Translate(move);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isRunning = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player zszedł z windy.");
        }
    }
}
