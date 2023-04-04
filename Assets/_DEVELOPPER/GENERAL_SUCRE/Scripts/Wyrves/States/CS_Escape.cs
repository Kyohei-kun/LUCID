using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Escape : MonoBehaviour, CS_I_State
{
    CS_Wyrve parent;
    Quaternion newRotation;
    float startTime;
    Transform player;

    public void State_Start(CS_Wyrve parent)
    {
        Vector3 newDirection;
        
        this.parent = parent;
        startTime = Time.time;
        player = parent.Manager.GetPlayerGameObject().transform;

        newDirection = Quaternion.Euler(0, Random.Range(-90, 90), 0) * -parent.Manager.GetMainIsland().forward;
        newDirection.y = 1;
        newDirection = newDirection.normalized;
        newRotation = Quaternion.LookRotation(newDirection);
        Debug.DrawRay(parent.transform.position, newDirection * 100, Color.red, 1000);
    }

    public void State_Update()
    {
        parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, newRotation, 0.05f);
        parent.transform.Translate(parent.transform.forward * parent.Speed, Space.World);
    }

    public CS_I_State State_Pass()
    {
        if(Vector3.Distance(parent.transform.position, player.position) > parent.DisctanceTriggerPlayer+20)
        {
            return new CS_Patrouille();
        }
        return null;
    }

    public void State_Finish()
    {
        //Supprimer Target ressource
    }

    public new string ToString()
    {
        return "Escape";
    }

}
