using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SCO_", menuName = "ScriptableObjects/RecipeCraft", order = 1)]
public class CS_Recipe : ScriptableObject
{
    public List<E_Item> ingredients;
    public GameObject prefab;
}

public enum E_Item
{
    Cristal,
    Scrap,
    Metal
}
