using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Zad1 : MonoBehaviour
{
    List<Vector3> positions = new List<Vector3>();
    public float delay = 3f;
    int objectCounter = 0;
    // obiekt do generowania
    public GameObject block;
    //-----------------------
    public int ileObiektow;
    public Material[] materialy;
    System.Random rand = new System.Random();

    void Start()
    {
        GameObject area = GameObject.Find("Arena");
        int positionX = (int)area.transform.position.x;
        int scaleX = (int)area.transform.localScale.x;
        int positionZ = (int)area.transform.position.z;
        int scaleZ = (int)area.transform.localScale.z;

         // w momecie uruchomienia generuje 10 kostek w losowych miejscach
        List<int> pozycje_x = new List<int>(Enumerable.Range((positionX - (scaleX * 5))+1, positionX + (scaleX * 5)).OrderBy(x => Guid.NewGuid()).Take(ileObiektow));
        List<int> pozycje_z = new List<int>(Enumerable.Range((positionZ - (scaleZ * 5))+1, positionX + (scaleZ * 5)).OrderBy(x => Guid.NewGuid()).Take(ileObiektow));

        for (int i = 0; i < ileObiektow; i++)
            this.positions.Add(new Vector3(pozycje_x[i], 5, pozycje_z[i]));
        // uruchamiamy coroutine
        StartCoroutine(GenerujObiekt());
    }

    IEnumerator GenerujObiekt()
    {
        foreach (Vector3 pos in positions)
        {
            this.block.GetComponent<Renderer>().material = materialy[rand.Next(0, materialy.Length)];
            Instantiate(this.block, this.positions.ElementAt(this.objectCounter++), Quaternion.identity);
            yield return new WaitForSeconds(this.delay);
        }
        // zatrzymujemy coroutine
        StopCoroutine(GenerujObiekt());
    }
}
