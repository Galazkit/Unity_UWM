using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zad2 : MonoBehaviour
{
    private GameObject player;
    public float distance = 3f;
    float distanceFromPlayer;
    
    bool isOpening = false;
   
    public bool left = false;
    bool isOpen = false;

    public int speed = 2;

    float stop;
    float start;


    // Start is called before the first frame update
    void Start()
    {
        start = transform.position.x;
        if (left)
        {
            speed = -speed;
            stop = transform.position.x - distance;
        }
        else
        {
            stop = transform.position.x + distance;
        }

        player = GameObject.FindWithTag("Player");

        distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceFromPlayer < 2f && transform.position.x <= stop)
        {
            isOpening = true;
        }

        if (isOpening)
        {
            Vector3 move = transform.right * speed * Time.deltaTime;
            transform.Translate(move);
        }

        if (transform.position.x >= stop && isOpen == false)
        {
            isOpening = false;
            isOpen = true;
        }

        if (distanceFromPlayer > distance + 2f && transform.position.x >= stop)
        {
            speed = -speed;
            isOpening = true;
        }

        if (transform.position.x <= start && isOpen == true)
        {
            isOpening = false;
            isOpen = false;
            speed = -speed;
        }

        Debug.Log(distanceFromPlayer);
    }
}
