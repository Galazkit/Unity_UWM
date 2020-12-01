using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{

    public enum ResourceTypes { Stone};
    public ResourceTypes resourceType;

    public float harvestTime;
    public float availableResource;

    public int gathers;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ResourceTick());
    }

    // Update is called once per frame
    void Update()
    {
        if(availableResource <=0)
        {
            Destroy(gameObject);
        }
    }

    public void ResourceGeather()
    {
        if(gathers != 0)
        {
            availableResource -= gathers;
        }
    }

    IEnumerator ResourceTick()
    {
         while(true)
        {
            yield return new WaitForSeconds(1);
            ResourceGeather();
        }
    }
}
