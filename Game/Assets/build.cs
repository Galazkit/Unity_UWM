using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class build: MonoBehaviour
{
    public GameObject builds;
    // Start is called before the first frame update


    public void spawn()
    {
        Instantiate(builds);
    }
}
