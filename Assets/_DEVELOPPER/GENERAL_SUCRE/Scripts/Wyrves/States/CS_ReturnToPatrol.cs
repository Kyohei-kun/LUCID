using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ReturnToPatrol : MonoBehaviour, CS_I_State
{
    Quaternion startRotation;
    CS_Wyrve parent;
    Vector3 forwardIsland;
    Vector3 newDirection;
    Quaternion newRotation;

    public void State_Start(CS_Wyrve parent)
    {
        this.parent = parent;
        startRotation = parent.transform.rotation;
        forwardIsland = -parent.Manager.GetMainIsland().forward; //Island a l'envers

        newDirection = Quaternion.Euler(0, Random.Range(-90,90), 0) * forwardIsland;
        newDirection.y = 1;
        newDirection = newDirection.normalized;
        newRotation = Quaternion.LookRotation(newDirection);
        Debug.DrawRay(parent.transform.position, newDirection * 100, Color.red, 1000);
    }

    public void State_Update()
    {
        parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, newRotation, 0.1f);
        parent.transform.Translate(parent.transform.forward * parent.Speed, Space.World);
    }

    public CS_I_State State_Pass()
    {
        return null;
    }

    public void State_Finish()
    {

    }

    public new string ToString()
    {
        return "Return to Patrol";
    }

}
