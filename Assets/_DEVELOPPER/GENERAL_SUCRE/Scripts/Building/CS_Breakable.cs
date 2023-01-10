using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Breakable : MonoBehaviour
{
    protected bool isOperationnel;
    [SerializeField] protected bool isIndestructible;
    protected int structuralState;
    protected int maxlife;

    public void MakeDammage(int dammage)
    {
        if (!isIndestructible)
        {
            structuralState -= dammage;
            structuralState = Mathf.Clamp(structuralState, 0, maxlife);

            if (structuralState == 0)
            {
                isOperationnel = false;
            }
        }
    }

    public bool Repare(int Undammage)
    {
        if (structuralState < maxlife)
        {
            structuralState += Undammage;
            isOperationnel = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}
