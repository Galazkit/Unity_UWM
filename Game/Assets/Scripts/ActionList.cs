using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActionList : MonoBehaviour
{
    public void Move(NavMeshAgent agent, RaycastHit hit)
    {
        agent.destination = hit.point;
    }
}
