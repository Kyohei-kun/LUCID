using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CS_Scrap : CS_Item
{
    private new void Start()
    {
        base.Start();
        enumItem = E_Item.Scrap;

        List<Transform> childs = GetComponentsInChildren<Transform>().ToList();

        foreach (var item in childs)
        {
            item.gameObject.SetActive(false);
        }
        int index = Random.Range(1, childs.Count);


        childs[index].gameObject.SetActive(true);
        childs[0].gameObject.SetActive(true);

    }
}
