using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Pique : MonoBehaviour, CS_I_State
{
    CS_Wyrve parent;
    GameObject target;
    float startTime;
    float distanceTriggerRedress = 100;

    public void State_Start(CS_Wyrve parent)
    {
        this.parent = parent;
        target = GameObject.FindGameObjectWithTag("Target");
        startTime = Time.time;
    }

    public void State_Update()
    {
        parent.SpeedFactorPique = Mathf.Clamp((Time.time - startTime), 1, 1);
        Quaternion piqueQuatTarget = Quaternion.LookRotation(target.transform.position - parent.transform.position);
        Quaternion flatQuat = piqueQuatTarget;

        flatQuat.eulerAngles = new Vector3(0, flatQuat.eulerAngles.y, flatQuat.eulerAngles.z);

        if (Vector3.Distance(parent.transform.position, target.transform.position) > distanceTriggerRedress)
        {
            parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, piqueQuatTarget, 0.1f);
            parent.transform.Translate(parent.transform.forward * parent.Speed * Mathf.Clamp((Time.time - startTime), 1, Mathf.Infinity), Space.World);
        }
        else
        {
            //Debug.Log(Vector3.Distance(parent.transform.position, target.transform.position).Remap(0, 500, 1, 0) + " |||| " + Vector3.Distance(parent.transform.position, target.transform.position));
            parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, flatQuat, Vector3.Distance(parent.transform.position, target.transform.position).Remap(0,distanceTriggerRedress,1,0));
            parent.transform.Translate((target.transform.position-parent.transform.position).normalized * parent.Speed * Mathf.Clamp((Time.time - startTime), 1, Mathf.Infinity), Space.World);

            if(parent.transform.position.y < target.transform.position.y)
            {
                parent.transform.Translate((target.transform.position - parent.transform.position), Space.World);
            }
        }
    }

    public CS_I_State State_Pass()
    {
        if (Vector3.Distance(parent.transform.position, target.transform.position) < 0.5f)
        {
            return new CS_Catch();
        }

        return null;
    }

    public void State_Finish()
    {
    }

    public new string ToString()
    {
        return "Pique";
    }

}
