using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Building : CS_Breakable
{
    protected bool isWork;

    public bool IsWork { get => isWork; set => isWork = value; }

    public void StartWork()
    {
        IsWork = true;
    }

    public void StopWork()
    {
        IsWork = false;
    }
}
