using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CS_PlayerTriggerItem : MonoBehaviour
{
    private List<CS_Item> items;

    private void Start()
    {
        items = new List<CS_Item>();
    }

    private void OnTriggerEnter(Collider other)
    {
        CS_Item currentItem = other.GetComponent<CS_Item>();

        if(currentItem != null && !items.Contains(currentItem))
        {
            items.Add(currentItem);
        }
        CleanList();
    }

    private void OnTriggerExit(Collider other)
    {
        CS_Item currentItem = other.GetComponent<CS_Item>();

        if (currentItem != null && items.Contains(currentItem))
        {
            items.Remove(currentItem);
        }

    }

    public CS_Item GetItem()
    {
        if(items.Count > 1)
        {
            items.OrderBy(go => Vector3.Distance(gameObject.transform.position, go.transform.position)); //go.transform.position.y.ToArray());
        }
        else if(items.Count == 0)
        {
            return null;
        }
        return items[0];
    }

    private void CleanList()
    {
        foreach (var item in items.ToList())
        {
            if(item == null)
            {
                items.Remove(item);
            }
        }
    }
}
