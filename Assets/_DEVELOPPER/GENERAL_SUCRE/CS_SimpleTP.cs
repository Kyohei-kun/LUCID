using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SimpleTP : MonoBehaviour
{
    [SerializeField] GameObject newPosition;

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.position = newPosition.transform.position;
        }
    }
}
