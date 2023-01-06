using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SocleBattery : MonoBehaviour
{
    CS_Battery currentBattery;
    [SerializeField] CS_ElectricBuilding electricBuilding;
    [Space(10)]
    [Header("Decal Socket Battery")]

    [SerializeField] Transform socket;

    [Space(5)]
    [SerializeField] Vector3 decalSmallBattery;
    [SerializeField] Vector3 decalLargeBattery;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Battery" && currentBattery == null && other.GetComponent<CS_Battery>().IsInHand == false)
        {
            currentBattery = other.GetComponent<CS_Battery>();
            electricBuilding.Battery = currentBattery;
            currentBattery.transform.position = socket.position + (currentBattery.SizeItem == SizeItem.Small ? decalSmallBattery : decalLargeBattery);
            currentBattery.transform.localEulerAngles = Vector3.zero;
            currentBattery.GetComponent<Rigidbody>().isKinematic = true;
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
