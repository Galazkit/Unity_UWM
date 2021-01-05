using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ObjectInfo : MonoBehaviour
{
    #region Node Values
    public enum ResourceTypes { Wood, Stone, Iron, Population };
    public ResourceTypes resourceType;

    public float availableResource;
    public int gathers;
    #endregion

    #region Unit values
    public ResourceManager RM;

    public ResourceTypes heldResourceType;

    public bool isGathering = false; // czy obecnie coś wydobywa
    public bool isGatherer = false; // czy jest liczony jako zbieracz

    public int heldResource;
    public int maxHeldResources;

    public GameObject[] drops;
    public GameObject targetNode;
    #endregion

    public enum ObjectTypes { Node, Building, Unit};
    public ObjectTypes objectType;

    public enum Ranks { Recruit }
    public Ranks rank;

    public enum TaskList { Gathering, Moving, Idle, Building, Attacking, Delivering }
    public TaskList task;

    public GameObject selectionIndidcator;
    public GameObject iconCam;
    public GameObject target;

    public bool isPrimary = false;
    public bool isSelected = false;
    public bool isWorker;
    public bool isUnit;
    public bool isPlayerObject;
    public bool isAllyObject;
    public bool canAttack;

    public string objectName;

    public float health;
    public float maxHealth;
    public float patk;
    public float pdef;
    public float kills;
    public float distToTarget;

    public float attackSpeed;
    public float range;

    private NavMeshAgent agent;

    //internal bool isSelected { get; set; }

    void Start()
    {
        health = maxHealth;

        StartCoroutine(AttackTick());

        if (objectType == ObjectTypes.Node || isWorker)
        {
            StartCoroutine(OneTick());
        }

        agent = GetComponent<NavMeshAgent>();
        iconCam = GetComponentInChildren<Camera>().gameObject;
    }

    void Update()
    {
        #region Units Functions
        if (isWorker)
        {

            if (transform.position == agent.destination && task == TaskList.Moving)
            {
                task = TaskList.Idle;
            }

            if (task == TaskList.Gathering)
            {
                distToTarget = Vector3.Distance(transform.position, target.transform.position);
                //Debug.Log(distToTarget);
                if (distToTarget <= 3.5f)
                {
                    Gather();
                }
            }

            if (task == TaskList.Delivering)
            {
                if (distToTarget <= 3.5f)
                {
                    if (heldResourceType == ResourceTypes.Wood)
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
                            agent.destination = target.transform.position;
                            isGatherer = false;
                        }
                        else
                        {
                            RM.wood += heldResource;
                            heldResource = 0;
                            task = TaskList.Gathering;
                            agent.destination = target.transform.position;

                            isGatherer = false;
                        }
                    }
                    if (heldResourceType == ResourceTypes.Stone)
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
                            agent.destination = target.transform.position;
                            isGatherer = false;
                        }
                        else
                        {
                            RM.stone += heldResource;
                            heldResource = 0;
                            task = TaskList.Gathering;
                            agent.destination = target.transform.position;

                            isGatherer = false;
                        }
                    }
                    if (heldResourceType == ResourceTypes.Iron)
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
                            agent.destination = target.transform.position;
                            isGatherer = false;
                        }
                        else
                        {
                            RM.iron += heldResource;
                            heldResource = 0;
                            task = TaskList.Gathering;
                            agent.destination = target.transform.position;

                            isGatherer = false;
                        }
                    }
                    if (heldResourceType == ResourceTypes.Population)
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
                            agent.destination = target.transform.position;
                            isGatherer = false;
                        }
                        else
                        {
                            RM.population += heldResource;
                            heldResource = 0;
                            task = TaskList.Gathering;
                            agent.destination = target.transform.position;

                            isGatherer = false;
                        }
                    }
                }
            }
            if (target == null && task == TaskList.Gathering) {
                {
                    if (heldResource != 0)
                    {
                        drops = GameObject.FindGameObjectsWithTag("Drops");
                        agent.destination = GetClosestDropOff(drops).transform.position;

                        distToTarget = Vector3.Distance(GetClosestDropOff(drops).transform.position, transform.position);

                        drops = null;
                        task = TaskList.Delivering;
                        target = null;
                    }
                    else
                    {
                        task = TaskList.Idle;
                        target = null;
                    }
                }
            }
            if (heldResource >= maxHeldResources)
            {

                target.GetComponent<ObjectInfo>().gathers--;
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
        }
        #endregion

        if (objectType != ObjectTypes.Node)
        {
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
        if (objectType == ObjectTypes.Unit) { 
            if (transform.position == agent.destination && task == TaskList.Moving)
            {
                task = TaskList.Idle;
            }
        }
        if (task == TaskList.Attacking)
        {
            if (target)
            {
                distToTarget = Vector3.Distance(target.transform.position, transform.position);

                if (distToTarget >= range)
                {
                    canAttack = false;
                    agent.destination = target.transform.position;
                }
                else if (distToTarget <= range)
                {
                    canAttack = true;
                }
            }
        }
        
        if (!target)
        {
            canAttack = false;
            task = TaskList.Idle;
        }

        if (objectType == ObjectTypes.Node && availableResource <= 0)
        {
                Destroy(gameObject);
        }

        if (Input.GetMouseButtonDown(1) && GetComponent<ObjectInfo>().isSelected)
        {
            RightClick();
        }

        selectionIndidcator.SetActive(!isSelected);

        iconCam.SetActive(isPrimary);
    }
    public void ResourceGeather()
    {
        if (gathers != 0)
        {
            availableResource -= gathers;
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
                    target.GetComponent<ObjectInfo>().gathers--;
                    isGathering = false;
                    isGatherer = false;
                }
                Move(agent, hit);
                task = TaskList.Moving;
            }
            else if (hit.collider.tag == "Resource")
            {
                Move(agent, hit);
                target = hit.collider.gameObject;
                task = TaskList.Gathering;
            }
            else if (hit.collider.tag == "Drops")
            {
                target.GetComponent<ObjectInfo>().gathers--;
                isGathering = false;
                drops = GameObject.FindGameObjectsWithTag("Drops");
                agent.destination = GetClosestDropOff(drops).transform.position;
                distToTarget = Vector3.Distance(GetClosestDropOff(drops).transform.position, transform.position);
                drops = null;
                task = TaskList.Delivering;
            }
            else if(hit.collider.tag == "Selectable")
            {
                ObjectInfo hitObject = hit.collider.GetComponent<ObjectInfo>();

                if(hitObject.isPlayerObject == false && hitObject.isAllyObject == false)
                {
                    target = hit.collider.gameObject;
                    task = TaskList.Attacking;
                }
            }
        }
    }

    public void Move(NavMeshAgent agent, RaycastHit hit)
    {
        agent.destination = hit.point;
    }

    public void Gather()
    {
        isGathering = true;
        if (!isGatherer)
        {
            target.GetComponent<ObjectInfo>().gathers++;
            isGatherer = true;
        }
        heldResourceType = target.GetComponent<ObjectInfo>().resourceType;

        //GetComponent<NavMeshObstacle>().enabled = true;
        //GetComponent<NavMeshAgent>().enabled = false;
    }

    private void OnEnable()
    {
        InputManager.selectedObjects.Add(this);
    }

    private void OnDisable()
    {
        InputManager.selectedObjects.Remove(this);
    }
    IEnumerator OneTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if(objectType == ObjectTypes.Node)
            {
                ResourceGeather();
            }

            if (isWorker)
            {
                if (isGathering)
                {
                    heldResource++;
                }
            }
        }
    }

    IEnumerator AttackTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackSpeed);

            if (canAttack)
            {
                ObjectInfo targetInfo = target.GetComponent<ObjectInfo>();

                targetInfo.health -= Mathf.Round(patk * (1 - (targetInfo.pdef * 0.05f)));

                Debug.Log(targetInfo.health);
            }
        }
    }
}
