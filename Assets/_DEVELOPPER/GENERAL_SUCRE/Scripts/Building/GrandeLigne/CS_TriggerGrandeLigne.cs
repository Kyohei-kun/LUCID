using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CS_TriggerGrandeLigne : MonoBehaviour, I_Interact
{
    GameObject player;
    public string UI_Name => "Grand Line";
    [SerializeField] CS_GrandeLigne grandeLigne;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Interract()
    {
        if(grandeLigne.IsWork)
            grandeLigne.StopWork();
        else
            grandeLigne.StartWork();
    }

    public bool PlayerIsInTrigger()
    { 
            List<Collider> colliders = Physics.OverlapBox(gameObject.transform.position, gameObject.transform.localScale).ToList();
            return colliders.Contains(player.GetComponent<Collider>());
    }

}
