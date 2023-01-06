using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Charger : CS_ElectricBuilding
{
    [SerializeField] float chargerValue;
    
    public new void StartWork()
    {
        if(battery != null)
        {
            battery.Charge(chargerValue);
        }
    }
}
