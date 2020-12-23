using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ObjectInfo : MonoBehaviour
{
    public GameObject selectionIndidcator;
    public GameObject iconCam;

    public bool isPrimary = false;
    public bool isSelected = false;
    public bool isUnit;

    public string objectName;

    public float health;
    public float maxHealth;
    public float patk;
    public float pdef;
    public float kills;

    public enum Ranks {Recruit}

    public Ranks rank;
    void Start()
    {
        iconCam = GetComponentInChildren<Camera>().gameObject;
    }

    void Update()
    {
        selectionIndidcator.SetActive(isSelected);

        if(health <= 0)
        {
            Destroy(gameObject);
        }

        iconCam.SetActive(isPrimary);
    }
}
