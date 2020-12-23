using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIManager : MonoBehaviour
{
     public ObjectInfo primary;

     public CanvasGroup UnitPanel;

     public Text nameDisp;

     public Slider HB;
     public Text healthDisp;

     public Text patkDisp;
     public Text pdefDisp;
     public Text rankDisp;
     public Text killDisp;


     // Start is called before the first frame update
     void Start()
     {
         UnitPanel = GameObject.Find("UnitPanel").GetComponent<CanvasGroup>();
         HB = GameObject.Find("HealthBar").GetComponent<Slider>();
         nameDisp = GameObject.Find("UnitName").GetComponent<Text>();
         healthDisp = GameObject.Find("HealthDisp").GetComponent<Text>();
         patkDisp = GameObject.Find("PATKDisp").GetComponent<Text>();
         pdefDisp = GameObject.Find("PDEFDisp").GetComponent<Text>();
         rankDisp = GameObject.Find("RankDesp").GetComponent<Text>();
         killDisp = GameObject.Find("KillCountDisp").GetComponent<Text>();
     }

     // Update is called once per frame
     void Update()
     {
         primary = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>().selectedInfo;

            HB.maxValue = primary.maxHealth;
            HB.value = primary.health;

            nameDisp.text = primary.objectName;
            healthDisp.text = "HP: " + primary.health;
            patkDisp.text = "PATK: " + primary.patk;
            pdefDisp.text = "PDED: " + primary.pdef;
            rankDisp.text = "" + primary.rank;
            killDisp.text = "Kills: " + primary.kills;
    
     }
    
}
