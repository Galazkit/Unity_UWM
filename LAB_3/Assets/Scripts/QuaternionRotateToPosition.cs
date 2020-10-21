using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaternionRotateToPosition : MonoBehaviour
{
    public Transform from ;
    public Transform to ;

    void Start()
    {

    }

    // krok rotacji, wyrazony w procentach, o jaki zostanie wykonana rotacja
    // w każdej klatce animacji
    private float percentage = 0.01f;

    void Update()
    {
        transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, percentage);
    }
}
