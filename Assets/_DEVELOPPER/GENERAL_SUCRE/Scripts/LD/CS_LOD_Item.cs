using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_LOD_Item : MonoBehaviour
{
    Transform player;

    [SerializedDictionary("Child", "Distance")]
    public SerializedDictionary<GameObject, float> prefabByDistance;

    GameObject currentChildShowed;

    public Transform Player { get => player; set => player = value; }

    public void ManualUpdate()
    {
        foreach (var item in prefabByDistance)
        {
            float d = Distance_P();
            if(d < item.Value || item.Value == 0)
            {
                if (currentChildShowed != null)
                {
                    currentChildShowed.SetActive(false);
                    currentChildShowed = null;
                }
                currentChildShowed = item.Key;
                currentChildShowed.SetActive(true);
                break;
            }
        }
    }

    private float Distance_P()
    {
        return Vector3.Distance(transform.position, Player.position);
    }
}