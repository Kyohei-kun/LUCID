using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CS_TriggerCraft : MonoBehaviour, I_Interact
{
    GameObject player;
    [SerializeField] CS_CraftTable craftTable;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public string UI_Name => "Craft Table";

    public void Interract()
    {
        craftTable.Craft();
    }

    public bool PlayerIsInTrigger()
    {
        List<Collider> colliders = Physics.OverlapBox(gameObject.transform.position, gameObject.transform.localScale).ToList();
        return colliders.Contains(player.GetComponent<Collider>());
    }
}
