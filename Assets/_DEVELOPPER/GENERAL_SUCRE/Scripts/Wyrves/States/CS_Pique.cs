using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Pique : MonoBehaviour, CS_I_State
{
    CS_Wyrve parent;
    GameObject target;
    float startTime;

    public void State_Start(CS_Wyrve parent)
    {
        this.parent = parent;
        target = GameObject.FindGameObjectWithTag("Target");
        startTime = Time.time;
        Debug.Log("Pique");
    }

    public CS_I_State State_Pass()
    {
        if(Vector3.Distance(parent.transform.position, target.transform.position) < 50)
        {
            return new CS_Theft();
        }

        return null;
    }
    public void State_Finish()
    {
    }

    public void State_Update()
    {
        parent.SpeedFactorPique = Mathf.Clamp((Time.time - startTime), 1, 50000);
        Quaternion quatTarget = Quaternion.LookRotation(target.transform.position - parent.transform.position);
        parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, quatTarget, 0.1f);
        parent.transform.Translate(parent.transform.forward * parent.Speed * Mathf.Clamp((Time.time - startTime), 1, 50000), Space.World);
    }
}
