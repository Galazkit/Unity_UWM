using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Units : MonoBehaviour
{

    public TaskList task;

    public ResourceManager RM;

    private ActionList AL;

    public GameObject targetNode;

    public NodeManager.ResourceTypes heldResourceType;

    public bool isGathering = false; // czy obecnie coś wydobywa
    public bool isGatherer = false; // czy jest liczony jako zbieracz

    private NavMeshAgent agent;

    public int heldResource;
    public int maxHeldResources;

    public GameObject[] drops;

    public float distToTarget;

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
        if(transform.position == agent.destination && task == TaskList.Moving)
        {
            task = TaskList.Idle;
        }

        if (task == TaskList.Gathering)
        {
            distToTarget = Vector3.Distance(transform.position, targetNode.transform.position);
            //Debug.Log(distToTarget);
            if (distToTarget <= 3.5f)
            {
                Gather();
            }
        }

        if(task == TaskList.Delivering)
        {
            if(distToTarget <= 3.5f)
            {
                if (heldResourceType == NodeManager.ResourceTypes.Wood)
                {
                    if (RM.wood >= RM.maxWood)
                    {
                        task = TaskList.Idle;
                        isGatherer = false;
                    }
                    else if (RM.wood + heldResource >= RM.maxWood)
                    {
                        int resourceOverflow = (int)RM.maxWood - (int)RM.wood;

                        heldResource -= resourceOverflow;
                        RM.wood = RM.maxWood;
                        task = TaskList.Gathering;
                        agent.destination = targetNode.transform.position;
                        isGatherer = false;
                    }
                    else
                    {
                        RM.wood += heldResource;
                        heldResource = 0;
                        task = TaskList.Gathering;
                        agent.destination = targetNode.transform.position;

                        isGatherer = false;
                    }
                }
                if (heldResourceType == NodeManager.ResourceTypes.Stone)
                {
                    if (RM.stone >= RM.maxStone)
                    {
                        task = TaskList.Idle;
                        isGatherer = false;
                    }
                    else if (RM.stone + heldResource >= RM.maxStone)
                    {
                        int resourceOverflow = (int)RM.maxStone - (int)RM.stone;

                        heldResource -= resourceOverflow;
                        RM.stone = RM.maxStone;
                        task = TaskList.Gathering;
                        agent.destination = targetNode.transform.position;
                        isGatherer = false;
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
                if (heldResourceType == NodeManager.ResourceTypes.Iron)
                {
                    if (RM.iron >= RM.maxIron)
                    {
                        task = TaskList.Idle;
                        isGatherer = false;
                    }
                    else if (RM.iron + heldResource >= RM.maxIron)
                    {
                        int resourceOverflow = (int)RM.maxIron - (int)RM.iron;

                        heldResource -= resourceOverflow;
                        RM.iron = RM.maxIron;
                        task = TaskList.Gathering;
                        agent.destination = targetNode.transform.position;
                        isGatherer = false;
                    }
                    else
                    {
                        RM.iron += heldResource;
                        heldResource = 0;
                        task = TaskList.Gathering;
                        agent.destination = targetNode.transform.position;

                        isGatherer = false;
                    }
                }
                if (heldResourceType == NodeManager.ResourceTypes.Population)
                {
                    if (RM.population >= RM.maxPopulation)
                    {
                        task = TaskList.Idle;
                        isGatherer = false;
                    }
                    else if (RM.population + heldResource >= RM.maxPopulation)
                    {
                        int resourceOverflow = (int)RM.maxPopulation - (int)RM.population;

                        heldResource -= resourceOverflow;
                        RM.population = RM.maxPopulation;
                        task = TaskList.Gathering;
                        agent.destination = targetNode.transform.position;
                        isGatherer = false;
                    }
                    else
                    {
                        RM.population += heldResource;
                        heldResource = 0;
                        task = TaskList.Gathering;
                        agent.destination = targetNode.transform.position;

                        isGatherer = false;
                    }
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
                if (isGathering)
                {
                    targetNode.GetComponent<NodeManager>().gathers--;
                    isGathering = false;
                    isGatherer = false;
                }
                AL.Move(agent, hit);
                task = TaskList.Moving;
            }
            else if (hit.collider.tag == "Resource")
            {
                AL.Move(agent, hit);
                targetNode = hit.collider.gameObject;
                task = TaskList.Gathering;
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
        heldResourceType = targetNode.GetComponent<NodeManager>().resourceType;

        //GetComponent<NavMeshObstacle>().enabled = true;
        //GetComponent<NavMeshAgent>().enabled = false;
    }
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
