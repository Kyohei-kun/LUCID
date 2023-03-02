using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_IA_Manager : MonoBehaviour
{
    List<CS_Wyrve> wyrves;
    GameObject player;

    public void Subscribe(CS_Wyrve wyrve)
    {
        if(wyrves == null)
            wyrves = new List<CS_Wyrve>();

        wyrves.Add(wyrve);
    }

    public GameObject GetPlayerGameObject()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        return player;
    }
}
