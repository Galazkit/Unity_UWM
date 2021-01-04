using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    float panSpeed = 5;
    float panDetect = 30;

    float panBorderSpeed = 0.1f;
    float scrollSpeed = 1f;
    //--------------------------------------
    [SerializeField] LayerMask mask;

    public bool hasPrimary;
    public bool circlePlaced = false;

    public CanvasGroup ObjectPanel;

    private Vector2 boxStart;
    private Vector2 boxEnd;

    public GameObject primaryObject;
    public GameObject selectionCircle;
    public GameObject selectionCirclePrefab;

    private Rect selectBox;
    public Texture boxTex;

    public ObjectInfo selectedInfo;
    private GameObject[] units;
    //------------------------------

    // public static List<ObjectInfo> selectedObjects = new List<ObjectInfo>();
    // public GameObject selectedObject;
    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        rotation = Camera.main.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        hasPrimary = primaryObject;
       // ObjectPanel = GameObject.Find("UnitPanel").GetComponent<CanvasGroup>();
        MoveCamera();

        if(Input.GetMouseButtonDown(0))
        {
            LeftClick();
        }

        if(Input.GetMouseButton(0) && boxStart == Vector2.zero)
        {
            boxStart = Input.mousePosition;
        }
        else if(Input.GetMouseButton(0) && boxStart != Vector2.zero)
        {
            boxEnd = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            boxStart = Vector2.zero;
            boxEnd = Vector2.zero;

            units = GameObject.FindGameObjectsWithTag("Selectable");
            MultiSelect();
            
        }
        selectBox = new Rect(boxStart.x, Screen.height - boxStart.y, boxEnd.x - boxStart.x, -1 * ((Screen.height - boxStart.y) - (Screen.height - boxEnd.y)));

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.transform.rotation = rotation;
        }
    }
    public void MultiSelect()
    {
        foreach(GameObject unit in units)
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
        boxStart = Vector2.zero;
        boxEnd = Vector2.zero;
    }
    public void LeftClick()
    {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         RaycastHit hit;

         if(Physics.Raycast(ray, out hit, 100))
         {
            //if (hit.collider.tag == "Ground")
            //{
            //    selectedInfo.isSelected = false;
            //    selectedObject = null;
            //    Debug.Log("Nie wybrano");
            //}
            if (hit.collider.tag == "Ground")
            {
                selectedInfo.isSelected = false;
                selectedInfo.isPrimary = false;
                primaryObject = null;

                units = GameObject.FindGameObjectsWithTag("Selectable");

                foreach (GameObject unit in units)
                {
                    unit.GetComponent<ObjectInfo>().isSelected = false;
                }
                selectedInfo = null;
                //Debug.Log("Nie wybrano");
            }

            //else if (hit.collider.tag == "Selectable")
            //{
            //    selectedObject = hit.collider.gameObject;
            //    selectedInfo = selectedObject.GetComponent<ObjectInfo>();

            //    selectedInfo.isSelected = true;
            //    Debug.Log("wybrano" + selectedInfo.objectName);
            //}
            else if(hit.collider.tag == "Selectable")
            {
                units = GameObject.FindGameObjectsWithTag("Selectable");

                foreach (GameObject unit in units)
                {
                    unit.GetComponent<ObjectInfo>().isSelected = false;
                }
                if (hasPrimary)
                {
                    selectedInfo.isSelected = false;
                    selectedInfo.isPrimary = false;
                    primaryObject = null;
                }

                primaryObject = hit.collider.gameObject;

                selectedInfo = primaryObject.GetComponent<ObjectInfo>();

                selectedInfo.isSelected = true;
                selectedInfo.isPrimary = true;

               // Debug.Log("wybrano" + selectedInfo.objectName);
            }
        }
    }
    void MoveCamera()
    {
        float CamPosX = Camera.main.transform.position.x;
        float CamPosY = Camera.main.transform.position.y;
        float CamPosZ = Camera.main.transform.position.z;

        float xPos = Input.mousePosition.x;
        float yPos = Input.mousePosition.y;

        Vector3 forwardMove = new Vector3(0, 0, 0);
        Vector3 lateralMove = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            forwardMove = Camera.main.transform.forward * panSpeed * CamPosY * Time.deltaTime;

            //moveZ += panSpeed * moveY  * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            forwardMove = -Camera.main.transform.forward * panSpeed * CamPosY * Time.deltaTime;
        }
        else if (yPos < Screen.height && yPos > Screen.height - panDetect)
        {
            forwardMove = Camera.main.transform.forward * panBorderSpeed * CamPosY * Time.deltaTime;
        }
        else if (yPos > 0 && yPos < panDetect)
        {
            forwardMove = -Camera.main.transform.forward * panBorderSpeed * CamPosY * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        { 
            lateralMove = -Camera.main.transform.right * panSpeed * CamPosY * Time.deltaTime; 
        }
        else if (Input.GetKey(KeyCode.D)) 
        {
            lateralMove = Camera.main.transform.right * panSpeed * CamPosY * Time.deltaTime;
        }
        else if (xPos > 0 && xPos < panDetect)
        {
            lateralMove = -Camera.main.transform.right * panBorderSpeed * CamPosY * Time.deltaTime;
        }
        else if (xPos < Screen.width && xPos > Screen.width - panDetect)
        {
            lateralMove = Camera.main.transform.right * panBorderSpeed * CamPosY * Time.deltaTime;
        }

        CamPosY -= Input.GetAxis("Mouse ScrollWheel") * (scrollSpeed * 100) * (CamPosY / 40); 
        forwardMove.y = 0;
        Vector3 move = lateralMove + forwardMove;

        CamPosY = Mathf.Clamp(CamPosY + move.y, 10, 40);
        CamPosX = Mathf.Clamp(CamPosX + move.x, -100, 100);
        CamPosZ = Mathf.Clamp(CamPosZ + move.z, -100, 100);

        Vector3 newPos = new Vector3(CamPosX, CamPosY, CamPosZ);
        Camera.main.transform.position = newPos;
    }

    void OnGUI()
    { 
        if(boxStart != Vector2.zero && boxEnd != Vector2.zero)
        {
            GUI.DrawTexture(selectBox, boxTex);
        }
    }
}
