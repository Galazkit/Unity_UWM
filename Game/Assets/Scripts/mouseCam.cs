using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCam : MonoBehaviour
{
    Vector2 mousePos;
    Vector2 smoothingVector;

    public float sensitivity = 5f;
    public float smoothing = 2f;

    public float rotateAmount = 2f;
    public float rotateSpeed = 100f;

    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            var mouseDir = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            mouseDir = Vector2.Scale(mouseDir, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

            smoothingVector.x = Mathf.Lerp(smoothingVector.x, mouseDir.x, 1f / smoothing);
            smoothingVector.y = Mathf.Lerp(smoothingVector.y, mouseDir.y, 1f / smoothing);

            mousePos += smoothingVector;

            mousePos.y = Mathf.Clamp(mousePos.y, -60, 90);


            transform.localRotation = Quaternion.AngleAxis(-mousePos.y, Vector3.right);

            Player.transform.localRotation = Quaternion.AngleAxis(mousePos.x, Player.transform.up);
        }
    }
}
