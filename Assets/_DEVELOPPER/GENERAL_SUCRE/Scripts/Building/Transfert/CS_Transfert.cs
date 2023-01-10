using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Transfert : CS_ElectricBuilding
{
    private CS_Battery receiverBattery;
    Coroutine co_Transfert;
    [SerializeField] float EU_By_Second;

    public void SetBattery(CS_Battery bat, bool isReceiver)
    {
        if (isReceiver)
        {
            receiverBattery = bat;
        }
        else
        {
            battery = bat;
        }

        CheckIfCanWork();
    }

    private void CheckIfCanWork()
    {
        if (battery != null && receiverBattery != null)
        {
            StartWork();
        }
        else
        {
            StopWork();
        }
    }

    public new void StartWork()
    {
        co_Transfert = StartCoroutine(TransfertEnergy());
    }

    public new void StopWork()
    {
        if(co_Transfert != null)
        StopCoroutine(co_Transfert);
    }

    IEnumerator TransfertEnergy()
    {
        while (true)
        {
            float valueTransfert = Time.deltaTime.Remap(0, 1, 0, EU_By_Second);
            if(battery.CurrentEnergy >= valueTransfert)
            battery.Charge(-receiverBattery.Charge(valueTransfert));
            else
            {
                battery.Charge(-receiverBattery.Charge(battery.CurrentEnergy));
            }

            yield return 0;
        }
    }
}
