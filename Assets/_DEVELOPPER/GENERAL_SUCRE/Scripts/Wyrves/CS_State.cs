using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CS_I_State
{
    public void State_Start(CS_Wyrve parent);

    public void State_Update();

    public CS_I_State State_Pass();

    public void State_Finish();

    public string ToString();
}
