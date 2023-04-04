using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Catch : MonoBehaviour, CS_I_State
{
    CS_Wyrve parent;
    GameObject target;
    bool escape = false;
    float timingPause = 1;
    float currentTimerPause = 0;

    public void State_Start(CS_Wyrve parent)
    {
        this.parent = parent;
        parent.gizmoDelegate.Add(Gizmo);
        target = GameObject.FindGameObjectWithTag("Target");
        target.transform.parent = parent.transform;
    }


    public void State_Update()
    {
            currentTimerPause += Time.deltaTime;
    }

    public CS_I_State State_Pass()
    {
        if (currentTimerPause > timingPause)
        {
            return new CS_Escape();
        }
        return null;
    }

    public void State_Finish()
    {
        parent.gizmoDelegate.Remove(Gizmo);
    }

    public new string ToString()
    {
        return "Catch";
    }

    private void Gizmo()
    {
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(Vector3.zero, 20);
    }
}
