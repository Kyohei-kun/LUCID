using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SimpleAngularVelocity : MonoBehaviour
{
    [SerializeField] Vector3 angularSpeed;

    void Update()
    {
        GetComponent<Rigidbody>().angularVelocity = angularSpeed;
    }
}
