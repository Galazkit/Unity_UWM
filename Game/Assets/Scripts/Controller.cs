using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    float panSpeed = 15;
    float panDetect = 15;

    float panBorderSpeed = 0.1f;
    float scrollSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float moveX = Input.GetAxis("Horizontal") * panSpeed * Time.deltaTime;
        //float moveZ = Input.GetAxis("Vertical") * panSpeed * Time.deltaTime;

        //float xPos = Input.mousePosition.x;
        //float yPos = Input.mousePosition.y;

        //if (Input.GetKey(KeyCode.A) || xPos > 0 && xPos < panDetect)
        //{
        //    moveX -= panSpeed * Time.deltaTime;
        //}

        //if (Input.GetKey(KeyCode.D) || xPos < Screen.width && xPos > Screen.width - panDetect)
        //{
        //    moveX += panSpeed * Time.deltaTime;
        //}

        //if (Input.GetKey(KeyCode.W) || yPos < Screen.height && yPos > Screen.height - panDetect)
        //{
        //    moveZ += panSpeed * Time.deltaTime;
        //}
        //else if (Input.GetKey(KeyCode.S) || yPos > 0 && yPos < panDetect)
        //{
        //    moveZ -= panSpeed * Time.deltaTime;
        //}

        //transform.Translate(new Vector3(moveX, Input.GetAxis("Mouse ScrollWheel") * panSpeed, moveZ));

        //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 10, 30), transform.position.z);

        //float CamPosX = Camera.main.transform.position.x;
        //float CamPosY = Camera.main.transform.position.y;//the lower the camera is, the slower the WASD will move the camera 
        //float CamPosZ = Camera.main.transform.position.z;

        //float xPos = Input.mousePosition.x;
        //float yPos = Input.mousePosition.y;

        //Vector3 forwardMove = new Vector3(0, 0, 0);
        //Vector3 lateralMove = new Vector3(0, 0, 0);

        //if (Input.GetKey(KeyCode.W)) //W
        //{
        //    forwardMove = Camera.main.transform.forward * panSpeed * CamPosY * Time.deltaTime;

        //    //moveZ += panSpeed * moveY  * Time.deltaTime;
        //}
        //else if (Input.GetKey(KeyCode.S))  //S
        //{
        //    forwardMove = -Camera.main.transform.forward * panSpeed * CamPosY * Time.deltaTime;
        //}
        //else if (yPos < Screen.height && yPos > Screen.height - panDetect) //W
        //{
        //    forwardMove = Camera.main.transform.forward * panBorderSpeed * CamPosY * Time.deltaTime;
        //}
        //else if (yPos > 0 && yPos < panDetect)  //S
        //{
        //    forwardMove = -Camera.main.transform.forward * panBorderSpeed * CamPosY * Time.deltaTime;
        //}

        //if (Input.GetKey(KeyCode.A))  //A
        //{ lateralMove = -Camera.main.transform.right * panSpeed * CamPosY * Time.deltaTime; }
        //else if (Input.GetKey(KeyCode.D)) //D
        //{
        //    lateralMove = Camera.main.transform.right * panSpeed * CamPosY * Time.deltaTime;
        //}
        //else if (xPos > 0 && xPos < panDetect)   //A
        //{
        //    lateralMove = -Camera.main.transform.right * panBorderSpeed * CamPosY * Time.deltaTime;
        //}
        //else if (xPos < Screen.width && xPos > Screen.width - panDetect) //D
        //{
        //    lateralMove = Camera.main.transform.right * panBorderSpeed * CamPosY * Time.deltaTime;
        //}

        //CamPosY -= Input.GetAxis("Mouse ScrollWheel") * (scrollSpeed * 100) * (CamPosY / 40); // 100 because without it the zoom was slow
        //forwardMove.y = 0;
        //Vector3 move = lateralMove + forwardMove;

        //CamPosY = Mathf.Clamp(CamPosY + move.y, 10, 40);
        //CamPosX = Mathf.Clamp(CamPosX + move.x, -100, 100);
        //CamPosZ = Mathf.Clamp(CamPosZ + move.z, -100, 100);//in this case y maps to the z (jest dobrze)


        //Vector3 newPos = new Vector3(CamPosX, CamPosY, CamPosZ);
        //Camera.main.transform.position = newPos;
    }
}
