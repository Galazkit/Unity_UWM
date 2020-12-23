using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Units : MonoBehaviour
{

    public TaskList task;
    public ResourceManager RM;

    private ActionList AL;

    GameObject targetNode;

    public NodeManager.ResourceTypes heldResourcesType;

    public bool isGathering = false;

    private NavMeshAgent agent;

    public int heldResource;
    public int maxHeldResources;

    public GameObject[] drops;

    public float distToTarget;

    public bool isGatherer = false;

    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GatherTick());
        agent = GetComponent<NavMeshAgent>();
        AL = FindObjectOfType<ActionList>();
    }

    // Update is called once per frame
    void Update()
    {
        if (task == TaskList.Gathering)
        {
            distToTarget = Vector3.Distance(transform.position, targetNode.transform.position);
            Debug.Log(distToTarget);

            if (distToTarget <= 3.5f)
            {
                Gather();
            }
        }

        if(task == TaskList.Delivering)
        {
            if(distToTarget <= 3.5f)
            {
                if (RM.stone >= RM.maxStone)
                {
                    task = TaskList.Idle;
                }
                else
                {
                    RM.stone += heldResource;
                    heldResource = 0;
                    task = TaskList.Gathering;
                    agent.destination = targetNode.transform.position;

                    isGatherer = false;
                }

            }
        }

        if (targetNode == null)
        {

            if (heldResource != 0)
            {
                drops = GameObject.FindGameObjectsWithTag("Drops");
                agent.destination = GetClosestDropOff(drops).transform.position;
                
                distToTarget = Vector3.Distance(GetClosestDropOff(drops).transform.position, transform.position);

                drops = null;
                task = TaskList.Delivering;
            }
            else
            {
                task = TaskList.Idle;
            }
        }
        if (heldResource >= maxHeldResources)
        {

            targetNode.GetComponent<NodeManager>().gathers--;
            isGathering = false;
            //Drop off point 
            drops = GameObject.FindGameObjectsWithTag("Drops");
            //finding the closest drop spot
            agent.destination = GetClosestDropOff(drops).transform.position;

            distToTarget = Vector3.Distance(GetClosestDropOff(drops).transform.position, transform.position);

            drops = null;

            task = TaskList.Delivering;

            //GetComponent<NavMeshObstacle>().enabled = false;
            //GetComponent<NavMeshAgent>().enabled = true;
        }

        if (Input.GetMouseButtonDown(1) && GetComponent<ObjectInfo>().isSelected)
        {
            RightClick();
        }
    }

    GameObject GetClosestDropOff(GameObject[] dropOffs)
    {
        GameObject closestDrop = null;
        float closestDistance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject targetDrop in dropOffs)
        {
            Vector3 direction = targetDrop.transform.position - position;
            float distance = direction.sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestDrop = targetDrop;
            }
        }
        return closestDrop; //return nearlest Drops
    }

    public void RightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.tag == "Ground")
            {
                targetNode.GetComponent<NodeManager>().gathers--;
                isGathering = false;

                AL.Move(agent, hit);
                task = TaskList.Moving;
            }
            else if (hit.collider.tag == "Resource")
            {
                AL.Move(agent, hit);
                targetNode = hit.collider.gameObject;
                task = TaskList.Gathering;

                //agent.destination = hit.collider.gameObject.transform.position;
                //Debug.Log("Wydobywam");
                //task = TaskList.Gathering;
                //targetNode = hit.collider.gameObject;
            }
            else if(hit.collider.tag == "Drops")
            {
                targetNode.GetComponent<NodeManager>().gathers--;
                isGathering = false;
                drops = GameObject.FindGameObjectsWithTag("Drops");
                agent.destination = GetClosestDropOff(drops).transform.position;

                distToTarget = Vector3.Distance(GetClosestDropOff(drops).transform.position, transform.position);

                drops = null;
                task = TaskList.Delivering;
            }
        }
    }
    public void Gather()
    {
        isGathering = true;
        if (!isGatherer)
        {
            targetNode.GetComponent<NodeManager>().gathers++;
            isGatherer = true;
        }

        heldResourcesType = targetNode.GetComponent<NodeManager>().resourceType;

        //GetComponent<NavMeshObstacle>().enabled = true;
        //GetComponent<NavMeshAgent>().enabled = false;
    }
    //public void OnTriggerEnter(Collider o)
    //{
    //    GameObject hitObject = o.gameObject;

    //    if (hitObject.tag == "Resource" && task == TaskList.Gathering)
    //    {
    //        isGathering = true;
    //        hitObject.GetComponent<NodeManager>().gathers++;
    //        heldResourcesType = hitObject.GetComponent<NodeManager>().resourceType;

    //       // GetComponent<NavMeshObstacle>().enabled = true;
    //       // GetComponent<NavMeshAgent>().enabled = false;

    //        // targetNode = hitObject;
    //    }
    //    else if (hitObject.tag == "Drops" && task == TaskList.Delivering)
    //    {
    //        if (RM.stone >= RM.maxStone)
    //        {
    //            task = TaskList.Idle;
    //        }
    //        else
    //        {
    //            RM.stone += heldResource;
    //            heldResource = 0;
    //            task = TaskList.Gathering;

    //            agent.destination = targetNode.transform.position;
    //        }
    //    }
    //}

    //public void OnTriggerExit(Collider o)
    //{
    //    GameObject hitObject = o.gameObject;

    //    if (hitObject.tag == "Resource")
    //    {
    //        hitObject.GetComponent<NodeManager>().gathers--;
    //        isGathering = false;
            
    //    }
    //}

    IEnumerator GatherTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (isGathering)
            {
                heldResource++;
            }
        }
    }
}
