using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zad_3 : MonoBehaviour
{

    //https://docs.unity3d.com/ScriptReference/Vector3.MoveTowards.html
    public float speed;
    public Vector3[] wierzcholki;
    public int wIndex = 0;
    void Start()
    {
    }

    void FixedUpdate()
    {
        if(wIndex == 4) wIndex = 0;
        if((Vector3)transform.position == wierzcholki[wIndex])
        {
            transform.Rotate(0,90,0);
            wIndex++;
        }

        transform.position = Vector3.MoveTowards(transform.position, wierzcholki[wIndex], speed * Time.deltaTime);
    }
}
