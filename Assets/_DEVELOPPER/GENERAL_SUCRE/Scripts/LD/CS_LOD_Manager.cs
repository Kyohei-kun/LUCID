using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CS_LOD_Manager : MonoBehaviour
{
    List<CS_LOD_Item> items;
    int indexCheck;

    Transform player;

    private void Start()
    {
        items = new List<CS_LOD_Item>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        AddSubscriptionItems();
    }

    public void AddSubscriptionItems()
    {
        List<GameObject> temp = GameObject.FindGameObjectsWithTag("LOD_Item").ToList();
        foreach (var item in temp)
        {
            items.Add(item.GetComponent<CS_LOD_Item>());
            items[items.Count - 1].Player = player;
        }
    }

    private void Update()//2 check per frame
    {
        items[indexCheck].ManualUpdate();
        items[(indexCheck+1)%items.Count].ManualUpdate();

        indexCheck += 2;
        if (indexCheck > items.Count) Debug.Log("EntireLoop " + Time.time);
        indexCheck = indexCheck % items.Count;
    }
}
