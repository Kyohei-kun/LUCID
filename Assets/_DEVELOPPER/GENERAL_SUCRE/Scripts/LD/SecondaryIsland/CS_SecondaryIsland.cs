using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SecondaryIsland : MonoBehaviour
{
    [SerializeField] List<Transform> spawners;
    CS_WorldIslandParameter worldParameter;
    [SerializeField] GameObject prefabCristal;

    private void Start()
    {
        worldParameter = GameObject.FindGameObjectWithTag("WorldIslandParameter").GetComponent<CS_WorldIslandParameter>();
        float height = transform.position.y;
        float nbCristaux = worldParameter.GetNbCristals(height);

        for (int i = 0; i < nbCristaux; i++)
        {
            GameObject temp = Instantiate(prefabCristal);
            temp.transform.parent = transform;
            temp.transform.position = spawners[Random.Range(0, spawners.Count)].position;
        }

        Destroy(this);
    }
}
