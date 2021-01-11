using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIManager : MonoBehaviour
{
    public ObjectInfo primary;

    public Slider HB;

    public Text nameDisp;
    public Text healthDisp;
    public Text patkDisp;
    public Text pdefDisp;
    public Text rankDisp;
    public Text killDisp;


    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
      //  primary = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>().selectedInfo;

        //if (primary)
        //{
        //    Debug.Log("jest");
        //    HB.maxValue = primary.maxHealth;
        //    HB.value = primary.health;

        //    nameDisp.text = primary.objectName;
        //    healthDisp.text = "HP: " + primary.health;
        //    patkDisp.text = "PATK: " + primary.patk;
        //    pdefDisp.text = "PDEF: " + primary.pdef;
        //    rankDisp.text = "" + primary.rank;
        //    killDisp.text = "Kills: " + primary.kills;
        //}
    }
}