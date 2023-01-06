using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SizeItem
{
    Small,
    Large
}

public class CS_Item : MonoBehaviour
{
    [SerializeField] protected SizeItem sizeItem;

    public void Taked()
    {
    }

    public void Dropped()
    {
    }
}
