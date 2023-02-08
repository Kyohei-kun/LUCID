using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SecondaryIsland : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabsVisuel;

    private void Start()
    {
        GameObject temp = Instantiate(prefabsVisuel[Random.Range(0, prefabsVisuel.Count - 1)]);
        temp.transform.parent = transform;
        temp.transform.localPosition = Vector3.zero;
    }
}
