using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PrepareAttack : MonoBehaviour, CS_I_State
{
    Vector3 startpoint;
    CS_Wyrve parent;
    Quaternion firstQuaternion;

    bool firstIsfinish = false;
    float timeStartState;

    public void State_Start(CS_Wyrve parent)
    {
        this.parent = parent;
        startpoint = parent.transform.position;
        firstQuaternion = parent.transform.rotation * Quaternion.Euler(new Vector3(-parent.AngleRise, 0, 0));
        parent.gizmoDelegate.Add(Gizmo);
        timeStartState = Time.time;
    }

    public void State_Update()
    {
        if (Quaternion.Angle(parent.transform.rotation, firstQuaternion) > 1 && !firstIsfinish)
        {
            parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, firstQuaternion, parent.SmoothAdjustement /*0.1*/);
        }
        else
        {
            firstIsfinish = true;
            parent.transform.rotation = parent.transform.rotation * Quaternion.Euler(parent.transform.InverseTransformDirection(Vector3.up));
        }

        parent.transform.Translate(parent.transform.forward * parent.Speed, Space.World);
    }

    public CS_I_State State_Pass()
    {
        parent.UpdatePlayerDistance();
        if((parent.Manager.GetPlayerGameObject().transform.position.y + parent.Height) < parent.transform.position.y )
        {
            return new CS_Pique();
        }
        if((Time.time - timeStartState) > 100)
        {
            return new CS_ReturnToPatrol();
        }
        return null;
    }
    public void State_Finish()
    {
        parent.gizmoDelegate.Remove(Gizmo);
    }

    private void Gizmo()
    {
        if (firstIsfinish)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(parent.transform.position, parent.DisctanceTriggerPlayer);
    }

    public new string ToString()
    {
        return "Prepare Attack";
    }

}
