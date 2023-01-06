using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SocleBattery : MonoBehaviour
{
    CS_Battery currentBattery;
    [SerializeField] CS_ElectricBuilding electricBuilding;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Battery")
        {
            currentBattery = other.GetComponent<CS_Battery>();
            electricBuilding.Battery = currentBattery;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Battery" && other.GetComponent<CS_Battery>() == currentBattery)
        {
            currentBattery = null;
            electricBuilding.Battery = null;
        }
    }
}
