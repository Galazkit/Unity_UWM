using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zad4 : MonoBehaviour
{
    // ruch wokół osi Y będzie wykonywany na obiekcie gracza, więc
    // potrzebna nam referencja do niego (konkretnie jego komponentu Transform)
    public Transform player;

    public float sensitivity = 200f;
    float horizontalRotation = 0;
    float verticalRotation = 0;

    void Start()
    {
        // zablokowanie kursora na środku ekranu, oraz ukrycie kursora
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalRotation += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        verticalRotation -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        verticalRotation = Mathf.Clamp(verticalRotation, -90, 90);
        player.transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);



    }
}
