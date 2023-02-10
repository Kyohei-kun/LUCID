using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_LOD_Item : MonoBehaviour
{
    GameObject player;

    [SerializeField] Dictionary<GameObject, float> prefabByDistance;

    public GameObject Player { get => player; set => player = value; }

    public void ManualUpdate()
    {
        foreach (var item in prefabByDistance)
        {
            Debug.Log("nya nya >:3");
        }
    }
}