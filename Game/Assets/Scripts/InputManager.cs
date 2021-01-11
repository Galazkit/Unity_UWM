using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InputManager : MonoBehaviour
{
    public float panSpeed;
    public float rotateSpeed;
    public float rotateAmount;

    private Quaternion rotation;

    private float panDetect = 20.0f;
    private float minHeight = 10.0f;
    private float maxHeight = 50.0f;

    public static List<ObjectInfo> selectedObjects = new List<ObjectInfo>();

    public bool hasPrimary;

    public CanvasGroup ObjectPanel;

     private Vector2 boxStart;
     private Vector2 boxEnd;

    public GameObject primaryObject;

    private Rect selectBox;
    public Texture boxTex;

    public ObjectInfo selectedInfo;
    private GameObject[] units;

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

        hasPrimary = primaryObject;
        units = GameObject.FindGameObjectsWithTag("Selectable");


        #region  mouse
        if (Input.GetMouseButton(0) && boxStart == Vector2.zero)
        {
            boxStart = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && boxStart != Vector2.zero)
        {
            boxEnd = Input.mousePosition;
        }
        if (Input.GetMouseButtonDown(0))
        {
            LeftClick();
        }

        if (Input.GetMouseButtonUp(0))
        {
            boxStart = Vector2.zero;
            boxEnd = Vector2.zero;

             MultiSelect();
        }
        selectBox = new Rect(boxStart.x, Screen.height - boxStart.y, boxEnd.x - boxStart.x, -1 * ((Screen.height - boxStart.y) - (Screen.height - boxEnd.y)));
        #endregion
        #region gui-select
        if (primaryObject != null)
        {
            ObjectPanel.alpha = 1;
            ObjectPanel.blocksRaycasts = true;
            ObjectPanel.interactable = true;
        }
        else
        {
            ObjectPanel.alpha = 0;
            ObjectPanel.blocksRaycasts = false;
            ObjectPanel.interactable = false;
        }
        #endregion
    }
    public void MultiSelect()
    {
        foreach (GameObject unit in units)
        {
            if (unit.GetComponent<ObjectInfo>().isUnit)
            {
                Vector2 unitPos = Camera.main.WorldToScreenPoint(unit.transform.position);

                if (selectBox.Contains(unitPos, true))
                {
                    if (!hasPrimary)
                    {
                        primaryObject = unit;
                        unit.GetComponent<ObjectInfo>().isPrimary = true;
                    }
                    unit.GetComponent<ObjectInfo>().isSelected = true;
                }
            }
        }
    }
    public void LeftClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
   
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.tag == "Ground")
            {
                Debug.Log("Ziemia");
                //foreach (GameObject unit in units)
                //{
                //    unit.GetComponent<ObjectInfo>().isSelected = false;
                //    unit.GetComponent<ObjectInfo>().isPrimary = false;
                //    primaryObject = null;
                //    selectedInfo = null;
                //}
                selectedInfo.isSelected = false;
                selectedInfo.isPrimary = false;
                selectedInfo = null;
                primaryObject = null;
                units = null;
            }
            else if (hit.collider.tag == "Selectable")
            {
                //foreach (GameObject unit in units)
                //{
                //    if (!hasPrimary)
                //    {
                //        primaryObject = unit;
                //        unit.GetComponent<ObjectInfo>().isPrimary = true;
                //    }
                //    unit.GetComponent<ObjectInfo>().isSelected = true;
                //}

                primaryObject = hit.collider.gameObject;
                selectedInfo = primaryObject.GetComponent<ObjectInfo>();
                if (!hasPrimary)
                {
                    selectedInfo.isPrimary = true;
                }
                selectedInfo.isSelected = true;

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

        if (Input.GetKey(KeyCode.A) || xPos > 0 && xPos < panDetect)
        {
            moveX -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) || xPos < Screen.width && xPos > Screen.width - panDetect)
        {
            moveX += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W) || yPos < Screen.height && yPos > Screen.height - panDetect)
        {
            moveZ += panSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S) || yPos > 0 && yPos < panDetect)
        {
            moveZ -= panSpeed * Time.deltaTime;
        }

        moveY -= Input.GetAxis("Mouse ScrollWheel") * (panSpeed * 3);

        moveY = Mathf.Clamp(moveY, minHeight, maxHeight);


        Vector3 newPos = new Vector3(moveX, moveY, moveZ);

        Camera.main.transform.position = newPos;
    }
    void RotateCamera()
    {
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destintion = origin;

        if (Input.GetMouseButton(2))
        {
            destintion.x -= Input.GetAxis("Mouse Y") * rotateAmount;
            destintion.y += Input.GetAxis("Mouse X") * rotateAmount;
        }

        if (destintion != origin)
        {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destintion, rotateSpeed * Time.deltaTime);
        }

    }

    void OnGUI()
    {
        if (boxStart != Vector2.zero && boxEnd != Vector2.zero)
        {
            GUI.DrawTexture(selectBox, boxTex);
        }
    }
}
