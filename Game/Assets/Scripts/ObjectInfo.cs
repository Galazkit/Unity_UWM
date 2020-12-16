using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ObjectInfo : MonoBehaviour
{
    public GameObject iconCam;
    public CanvasGroup InfoPanel;

    public bool isSelected = false;

    public string objectName;
    public Text nameDisp;

    public int health;
    public int maxHealth;
    public Slider HB;
    public Text healthDisp;

    public int patk;
    public Text patackDisp;
    public int pdef;
    public Text pdefDisp;

    public enum Ranks 
    { 
        //list ranks here 
        Recruit
    }
    public Ranks rank;
    public Text rankDisp;

    public int kills;
    public Text KillDisp;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }

        HB.maxValue = maxHealth;
        HB.value = health;

        iconCam.SetActive(isSelected);
        nameDisp.text = objectName;
        healthDisp.text = "HP: " + health;
        patackDisp.text = "PATK: " + patk;
        pdefDisp.text = "PDEF: " + pdef;
        rankDisp.text = "" + rank;
        KillDisp.text = "" + kills;

        if (isSelected)
        {
            InfoPanel.alpha = 1;
            InfoPanel.blocksRaycasts = true;
            InfoPanel.interactable = true;
            
        }
        else
        {
            InfoPanel.alpha = 0;
            InfoPanel.blocksRaycasts = false;
            InfoPanel.interactable = false;
        }
    }
}
