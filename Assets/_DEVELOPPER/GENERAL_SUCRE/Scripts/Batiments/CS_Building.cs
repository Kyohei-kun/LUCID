using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Building : CS_Breakable
{
    protected bool isWork;

    public void StartWork()
    {
        isWork = true;
    }

    public void StopWork()
    {
        isWork = false;
    }
}
