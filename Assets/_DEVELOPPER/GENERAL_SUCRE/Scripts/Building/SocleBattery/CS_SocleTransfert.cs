using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SocleTransfert : CS_SocleBattery
{
    [SerializeField] protected bool isReceiverSocle;

    protected new void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CS_Battery>() != null && currentBattery == null && other.GetComponent<CS_Battery>().IsInHand == false)
        {
            currentBattery = other.GetComponent<CS_Battery>();
            electricBuilding.GetComponent<CS_Transfert>().SetBattery(currentBattery, isReceiverSocle);
            currentBattery.transform.position = socket.position + (currentBattery.SizeItem == SizeItem.Small ? decalSmallBattery : decalLargeBattery);
            currentBattery.transform.localEulerAngles = Vector3.zero;
            currentBattery.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    protected new void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CS_Battery>() != null && other.GetComponent<CS_Battery>() == currentBattery)
        {
            currentBattery = null;
            electricBuilding.GetComponent<CS_Transfert>().SetBattery(null, isReceiverSocle);
        }
    }
}
