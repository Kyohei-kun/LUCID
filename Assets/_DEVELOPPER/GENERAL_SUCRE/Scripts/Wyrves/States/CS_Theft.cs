using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Theft : MonoBehaviour, CS_I_State
{
    int i = 200;
    CS_Wyrve parent;

    public void State_Start(CS_Wyrve parent)
    {
        parent.gizmoDelegate.Add(Gizmo);
    }
    public void State_Update()
    {
        Debug.Log("Update Theft");
    }

    public CS_I_State State_Pass()
    {
        Debug.Log("Pass Theft " + i);
        i--;
        if(i == 0)
        {
            return new CS_Patrouille();
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
