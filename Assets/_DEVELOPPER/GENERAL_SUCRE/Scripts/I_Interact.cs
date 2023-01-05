using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Interact
{
    public void Interract();
    public bool PlayerIsInTrigger();

    public string UI_Name { get;}
}
