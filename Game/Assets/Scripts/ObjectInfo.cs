using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectInfo : MonoBehaviour
{
    public NodeManager.ResourceTypes heldResourcesType;

    public bool isSelected = false;
    public bool isGathering = false;

    public string objectName;

    private NavMeshAgent agent;

    public int heldResource;
    public int maxHeldResources;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GatherTick());
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if(heldResource >= maxHeldResources)
        {
            //Drop off point here
        }

        if(Input.GetMouseButtonDown(1) && isSelected)
        {
            RightClick();
        }
    }

    public void RightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit,100))
        {
            if (hit.collider.tag == "Ground")
            {
                agent.destination = hit.point;
                Debug.Log("Lece!");
            }
            else if (hit.collider.tag == "Resource")
            {
                agent.destination = hit.collider.gameObject.transform.position;
                Debug.Log("Wydobywam");
            }
        }
    }

    public void OnTriggerEnter(Collider o)
    {
        GameObject hitObject = o.gameObject;

        if(hitObject.tag == "Resource")
        {
            isGathering = true;
            hitObject.GetComponent<NodeManager>().gathers++;
            heldResourcesType = hitObject.GetComponent<NodeManager>().resourceType;
        }
    }

    public void OnTriggerExit(Collider o)
    {
        GameObject hitObject = o.gameObject;

        if (hitObject.tag == "Resource")
        {
            hitObject.GetComponent<NodeManager>().gathers--;
        }
    }

    IEnumerator GatherTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if(isGathering)
            {
                heldResource++;
            }
        }
    }
}
