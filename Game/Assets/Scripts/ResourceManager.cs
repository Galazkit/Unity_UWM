using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public float wood;
    public float maxWood;
    public float stone;
    public float maxStone;
    public float iron;
    public float maxIron;
    public float population;
    public float maxPopulation;

    public Text woodDisp;
    public Text stoneDisp;
    public Text ironDisp;
    public Text populationDisp;

    private GameObject[] popCount;
    private int popCountInt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        popCount = GameObject.FindGameObjectsWithTag("Selectable");
        popCountInt = popCount.Length;

        woodDisp.text = "" + wood + "/" + maxWood;
        stoneDisp.text = "" + stone + "/" + maxStone;
        ironDisp.text = "" + iron + "/" + maxIron;
        populationDisp.text = "" + popCountInt + "/" + maxPopulation;
    }
}
