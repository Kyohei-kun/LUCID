using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CS_LOD_Manager : MonoBehaviour
{
    List<CS_LOD_Item> items;
    int indexCheck;


    private void Start()
    {
        AddSubscriptionItems();
    }

    public void AddSubscriptionItems()
    {
        List<GameObject> temp = GameObject.FindGameObjectsWithTag("LOD_Item").ToList();
        foreach (var item in temp)
        {
            items.Add(item.GetComponent<CS_LOD_Item>());
        }
    }

    private void Update()
    {
        items[indexCheck].ManualUpdate();
        if (indexCheck == items.Count - 1)
        {
            indexCheck = 0;
        }
        else
        {
            indexCheck++;
        }
    }
}
