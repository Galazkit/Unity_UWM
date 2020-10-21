using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zad_2 : MonoBehaviour
{
    public float speed;
    private int kierunek = 1;
    void Start()
    {

    }
    void Update()
    {
    if(transform.position.x >10) kierunek = -1;
    if(transform.position.x <0) kierunek = 1;

    transform.Translate(Vector3.right * speed * Time.deltaTime * kierunek);
    }
}
