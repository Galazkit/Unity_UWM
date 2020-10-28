using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zad_5 : MonoBehaviour
{
    public GameObject Cube;
    public int zakres_od;
    public int zakres_do;
    public int ile;

    void Start()
    {
        List<int[]> Pairs = generateRandomPairs(zakres_od, zakres_do);
        for (int i = 0; i < ile; i++)
        {
            int randPair = UnityEngine.Random.Range(zakres_od, Pairs.Count);
            Instantiate(Cube, new Vector3(Pairs[randPair][0], 1.0f, Pairs[randPair][1]), Quaternion.identity);
            Pairs.Remove(Pairs[randPair]);
        }
    }

    void Update()
    {
    }
    List<int[]> generateRandomPairs(int zakres_od, int zakres_do)
    {
        List<int[]> wynik = new List<int[]>();
        for (int i = zakres_od; i < zakres_do; i++)
            for (int j = zakres_od; j < zakres_do; j++)
                wynik.Add(new int[2] { i, j });

        return wynik;
    }
}
