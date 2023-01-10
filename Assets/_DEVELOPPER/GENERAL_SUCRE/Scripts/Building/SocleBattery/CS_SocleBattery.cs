using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SocleBattery : MonoBehaviour
{
    protected CS_Battery currentBattery;
    [SerializeField] protected CS_ElectricBuilding electricBuilding;
    [Space(10)]
    [Header("Decal Socket Battery")]

    [SerializeField] protected Transform socket;

    [Space(5)]
    [SerializeField] protected Vector3 decalSmallBattery;
    [SerializeField] protected Vector3 decalLargeBattery;


    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Battery" && currentBattery == null && other.GetComponent<CS_Battery>().IsInHand == false)
        {
            currentBattery = other.GetComponent<CS_Battery>();
            electricBuilding.SetBattery(currentBattery);
            currentBattery.transform.position = socket.position + (currentBattery.SizeItem == SizeItem.Small ? decalSmallBattery : decalLargeBattery);
            currentBattery.transform.localEulerAngles = Vector3.zero;
            currentBattery.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if(other.tag == "Battery" && other.GetComponent<CS_Battery>() == currentBattery)
        {
            currentBattery = null;
            electricBuilding.SetBattery(null);
        }
    }
}
