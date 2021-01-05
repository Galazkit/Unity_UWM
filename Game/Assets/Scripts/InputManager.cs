using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InputManager : MonoBehaviour
{
    float panSpeed = 5;
    float panDetect = 30;

    float panBorderSpeed = 0.5f;
    float scrollSpeed = 1f;
    //--------------------------------------

    public static List<ObjectInfo> selectedObjects = new List<ObjectInfo>();

    public Canvas canvas;

    public Image selectionBox;

    [SerializeField] LayerMask mask;

    public bool hasPrimary;
    public bool hasTolTip;

    public CanvasGroup ObjectPanel;

     private Vector2 boxStart;
     private Vector2 boxEnd;

    public GameObject primaryObject;
    public GameObject tolTip;

    private Rect selectBox;
    public Texture boxTex;

    public ObjectInfo selectedInfo;
    private GameObject[] units;


    private Vector3 startPos;
    private BoxCollider worldCollider;
    private RectTransform RT;
    private bool isSelecting;

    //------------------------------

    // public static List<ObjectInfo> selectedObjects = new List<ObjectInfo>();
    // public GameObject selectedObject;
    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        rotation = Camera.main.transform.rotation;
    }

    private void Awake()
    {
        if (canvas == null)
        {
            canvas = FindObjectOfType<Canvas>();
        }

        if (selectionBox != null)
        {
            RT = selectionBox.GetComponent<RectTransform>();
            RT.pivot = Vector2.one * 0.5f;
            RT.anchorMin = Vector2.one * 0.5f;
            RT.anchorMax = Vector2.one * 0.5f;
            selectionBox.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        hasPrimary = primaryObject;
        hasTolTip = tolTip;

        #region boxselect old
        if (Input.GetMouseButton(0) && boxStart == Vector2.zero)
        {
            boxStart = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && boxStart != Vector2.zero)
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
        #endregion



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

        if (Input.GetKeyDown(KeyCode.Space)) // resetowanie kamery
        {
            Camera.main.transform.rotation = rotation;
        }
    }
    //    // obsługa myszy

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        LeftClick();
    //    }

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        isSelecting = false;
    //    }

    //    selectionBox.gameObject.SetActive(isSelecting);

    //    if (isSelecting)
    //    {
    //        Bounds bounds = new Bounds();
    //        bounds.center = Vector3.Lerp(startPos, Input.mousePosition, 0.5f);
    //        bounds.size = new Vector3(
    //            Mathf.Abs(startPos.x - Input.mousePosition.x),
    //            Mathf.Abs(startPos.y - Input.mousePosition.y),
    //            0
    //            );
    //        RT.position = bounds.center;
    //        RT.sizeDelta = canvas.transform.InverseTransformVector(bounds.size);

    //        foreach (ObjectInfo selectable in selectedObjects)
    //        {
    //            if (selectable.isUnit && selectable.isPlayerObject)
    //            {
    //                Vector3 screenPos = Camera.main.WorldToScreenPoint(selectable.transform.position);
    //                screenPos.z = 0;
    //                UpdateSelection(selectable, bounds.Contains(screenPos));
    //            }
    //        }
    //    }
    //}
    //public void UpdateSelection(ObjectInfo selectedObject, bool value)
    //{
    //    if (selectedObject.isSelected != value)
    //    {
    //        if (value == false)
    //        {
    //            if (hasPrimary)
    //            {
    //                selectedObject.isPrimary = value;
    //                primaryObject = null;
    //                hasPrimary = value;
    //            }
    //            else
    //            {
    //                if (!hasPrimary)
    //                {
    //                    selectedObject.isPrimary = value;
    //                    primaryObject = selectedObject.gameObject;
    //                    hasPrimary = value;
    //                    selectedInfo = primaryObject.GetComponent<ObjectInfo>();
    //                }
    //            }

    //            selectedObject.isSelected = value;
    //        }
    //    }
    //}

    //List<ObjectInfo> GetSelected()
    //{
    //    return new List<ObjectInfo>(selectedObjects.Where(x => x.isSelected));
    //}

    //public void ClearSelection()
    //{
    //    selectedObjects.ForEach(x => x.isSelected = false);
    //    selectedObjects.ForEach(x => x.isPrimary = false);
    //}

    #region multisel old
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
        //boxStart = Vector2.zero;
        //boxEnd = Vector2.zero;
    }
    #endregion

    #region old Left click
    public void LeftClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
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
            }

            //else if (hit.collider.tag == "Selectable")
            //{
            //    selectedObject = hit.collider.gameObject;
            //    selectedInfo = selectedObject.GetComponent<ObjectInfo>();

            //    selectedInfo.isSelected = true;
            //    Debug.Log("wybrano" + selectedInfo.objectName);
            //}
            else if (hit.collider.tag == "Selectable")
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
    #endregion
    //public void LeftClick()
    //{
    //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     RaycastHit hit;

    //    if(Physics.Raycast(ray, out hit))
    //    {
    //        ObjectInfo OI = hit.collider.GetComponent<ObjectInfo>();

    //        if(OI != null)
    //        {
    //            if (Input.GetKey(KeyCode.LeftShift))
    //            {
    //                UpdateSelection(OI, !OI.isSelected);
    //            }
    //            else
    //            {
    //                ClearSelection();
    //                UpdateSelection(OI, true);
    //            }
    //        }
    //    }
    //    startPos = Input.mousePosition;
    //    isSelecting = true;
    //}

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
        if (boxStart != Vector2.zero && boxEnd != Vector2.zero)
        {
            GUI.DrawTexture(selectBox, boxTex);
        }
    }
}
