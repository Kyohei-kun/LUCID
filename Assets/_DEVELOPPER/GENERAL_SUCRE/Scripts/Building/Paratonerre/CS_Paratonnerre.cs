using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Paratonnerre : CS_ElectricBuilding
{
    [SerializeField] Animator animator;

    public new void StartWork()
    {
        base.StartWork();
        animator.SetBool("IsActive", true);
    }

    public new void StopWork()
    {
        base.StopWork();
        animator.SetBool("IsActive", false);
    }
}
