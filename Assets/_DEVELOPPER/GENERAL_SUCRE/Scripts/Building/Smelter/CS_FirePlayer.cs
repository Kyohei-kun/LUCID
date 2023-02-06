using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_FirePlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Fire1");
        if(other.tag == "Player")
        {
            Debug.Log("Fire2");
            //other.GetComponent<Rigidbody>().AddForce(Vector3.up * 10000);
        }
    }
}
