using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_UpdateHightSocketItem : MonoBehaviour
{

    [SerializeField] Transform handBone;

    void Update()
    {
        transform.position = new Vector3(transform.position.x, handBone.position.y, transform.position.z);
    }
}
