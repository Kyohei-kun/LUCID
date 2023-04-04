using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Patrouille : MonoBehaviour, CS_I_State
{
    int i = 600;

    Transform player;
    float radius = 50;
    Vector3 spawnPosition;
    Vector3 horizontalTargetDirection;
    Vector3 newHorizontalTargetDirection;
    Vector3 verticalTargetDirection;
    Vector3 targetPosition;
    Vector3 vectorIn;
    CS_Wyrve parent;

    bool isInZone;
    bool lastIsInZone = true;
    bool isInRedirection;

    public void State_Start(CS_Wyrve parent)
    {
        player = parent.Manager.GetPlayerGameObject().transform;
        parent.gizmoDelegate.Add(Gizmo);
        spawnPosition = parent.StartPosition;
        horizontalTargetDirection = Vector3.Scale(Random.insideUnitSphere, new Vector3(1, 0, 1)).normalized;
        this.parent = parent;
        parent.Animator.SetBool("IsFly", true);
    }

    public void State_Update()
    {
        isInZone = Vector3.Distance(parent.transform.position, parent.StartPosition) < parent.RadiusZone;

        if (!isInZone && lastIsInZone)
        {
            isInRedirection = true;
            vectorIn = Vector3.Scale(parent.StartPosition - parent.transform.position, new Vector3(1, 0, 1));
            vectorIn = Quaternion.AngleAxis(Random.Range(-15, 15), Vector3.up) * vectorIn;
        }
        if (isInZone && !lastIsInZone)
        {
            horizontalTargetDirection = Vector3.Scale(parent.transform.forward, new Vector3(1, 0, 1));
        }

        verticalTargetDirection = new Vector3(0, Mathf.Cos(Time.time * parent.Frequency) * parent.Amplitude, 0);

        Quaternion targetQuaternion = Quaternion.LookRotation((horizontalTargetDirection.normalized + verticalTargetDirection), Vector3.up);

        Quaternion redirectionTargetQuaternion = Quaternion.LookRotation((vectorIn + verticalTargetDirection).normalized, Vector3.up);

        if (!isInRedirection)
        {
            parent.transform.rotation = targetQuaternion;
        }
        else
        {
            parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, redirectionTargetQuaternion, parent.SmoothReturnMove);
            if (Quaternion.Angle(parent.transform.rotation, redirectionTargetQuaternion) < 1)
            {
                isInRedirection = false;
                horizontalTargetDirection = Vector3.Scale(vectorIn, new Vector3(1,0,1));
            }
        }

        //parent.transform.rotation = Quaternion.LookRotation(horizontalTargetDirection + verticalTargetDirection).normalized, Vector3.up);
        parent.transform.Translate(parent.transform.forward * parent.Speed, Space.World);


        lastIsInZone = isInZone;
        //if (Vector3.Distance(parent.transform.position + (targetDirection * 10), spawnPosition) > 100)
        //{
        //    targetDirection = -targetDirection;
        //}

        //targetDirection.y = Mathf.Cos(Time.time * parent.Frequency) * parent.Amplitude;

        //targetPosition = Vector3.Lerp(parent.transform.position, (parent.transform.position + (targetDirection * parent.Speed)), parent.SmoothMovement);

        ////parent.transform.rotation = Quaternion.Lerp(Quaternion.LookRotation(parent.transform.forward) , Quaternion.LookRotation(parent.transform.position + targetPosition), 0.1f );
        //parent.transform.rotation = Quaternion.LookRotation(parent.transform.position + targetPosition);
        ////parent.transform.LookAt(targetPosition);
        //parent.transform.position = targetPosition;
    }

    public CS_I_State State_Pass()
    {
        if(parent.DistancePlayer < parent.DisctanceTriggerPlayer)
        {
            return new CS_PrepareAttack();
        }
        parent.UpdatePlayerDistance();
        return null;
    }

    public void State_Finish()
    {
        parent.gizmoDelegate.Remove(Gizmo);
    }

    public new string ToString()
    {
        return "Patrouille";
    }

    private void Gizmo()
    {
        if (isInZone)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(spawnPosition, parent.RadiusZone);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(parent.transform.position, parent.DisctanceTriggerPlayer);
    }
}
