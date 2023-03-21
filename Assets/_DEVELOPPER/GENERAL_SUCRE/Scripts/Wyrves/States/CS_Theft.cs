using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Theft : MonoBehaviour, CS_I_State
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
    }



    public void State_Update()
    {
        if (Vector3.Distance(parent.transform.position, target.transform.position) < 1)
            escape = true;

        Vector3 eulerTarget = parent.transform.rotation.eulerAngles;
        eulerTarget.x = 0;
        Quaternion quatTarget = Quaternion.Euler(eulerTarget);

        parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, quatTarget, 0.2f);
        if(!escape)
        {
            parent.transform.Translate((target.transform.position - parent.transform.position).normalized * parent.Speed * parent.SpeedFactorPique, Space.World);
        }
        else
        {
            if(currentTimerPause < timingPause)
            {
                currentTimerPause += Time.deltaTime;
                return;
            }
            parent.transform.Translate(parent.transform.forward * parent.Speed, Space.World);
        }
    }

    public CS_I_State State_Pass()
    {
        if(Vector3.Distance(parent.transform.position, target.transform.position)< 5)
        {

        }
        return null;
    }
    
    public void State_Finish()
    {
        parent.gizmoDelegate.Remove(Gizmo);
    }

    public new string ToString()
    {
        return "Theft";
    }

    private void Gizmo()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Vector3.zero, 20);
    }
}
