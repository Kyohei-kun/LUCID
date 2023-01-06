using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ElectricBuilding : CS_Building
{
    protected CS_Battery battery;

    public CS_Battery Battery { get => battery; set => battery = value; }
}
