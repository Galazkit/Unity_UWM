using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public float panSpeed;
    public float rotateSpeed;
    public float rotateAmount;

    private Quaternion rotation;

    private float panDetect = 20.0f;
    private float minHeight = 10.0f;
    private float maxHeight = 50.0f;

    public GameObject selectedObject;

    private ObjectInfo selectedInfo;

    // Start is called before the first frame update
    void Start()
    {
        rotation = Camera.main.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        RotateCamera();

        if(Input.GetMouseButtonDown(0)) //left
        {
            LeftClick();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.transform.rotation = rotation;
        }
    }
    public void LeftClick()
    {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         RaycastHit hit;

         if(Physics.Raycast(ray, out hit, 100))
         {
            if(hit.collider.tag == "Ground")
            {
                selectedObject = null;
                Debug.Log("Nie wybrano");
            }
            else if(hit.collider.tag == "Selectable")
            {
                selectedObject = hit.collider.gameObject;
                selectedInfo = selectedObject.GetComponent<ObjectInfo>();

                selectedInfo.isSelected = true;
                Debug.Log("wybrano" + selectedInfo.objectName);
            }
         }
    }
    void MoveCamera()
    {

        float moveX = Camera.main.transform.position.x;
        float moveY = Camera.main.transform.position.y;
        float moveZ = Camera.main.transform.position.z;

        float xPos = Input.mousePosition.x;
        float yPos = Input.mousePosition.y;

        if(Input.GetKey(KeyCode.A)  || xPos > 0 && xPos < panDetect)
        {
            moveX -= panSpeed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.D)  || xPos < Screen.width && xPos > Screen.width - panDetect)
        {
            moveX += panSpeed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.W)  || yPos < Screen.height && yPos > Screen.height - panDetect)
        {
            moveZ += panSpeed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.S)  || yPos > 0 && yPos < panDetect)
        {
            moveZ -= panSpeed * Time.deltaTime;
        }

        moveY -= Input.GetAxis("Mouse ScrollWheel") * (panSpeed * 3 );
      
        moveY = Mathf.Clamp(moveY, minHeight, maxHeight);


        Vector3 newPos = new Vector3(moveX, moveY, moveZ);

        Camera.main.transform.position = newPos;
    }

    void RotateCamera()
    {
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destintion = origin;

        if(Input.GetMouseButton(2))
        {
            destintion.x -= Input.GetAxis("Mouse Y") * rotateAmount;
            destintion.y += Input.GetAxis("Mouse X") * rotateAmount;
        }

        if(destintion != origin)
        {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destintion, rotateSpeed * Time.deltaTime);
        }

    }
}
