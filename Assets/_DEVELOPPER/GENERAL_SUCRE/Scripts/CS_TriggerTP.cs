using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CS_TriggerTP : MonoBehaviour, I_Interact
{
    [SerializeField] GameObject newPosition;
    private GameObject player;

    public string UI_Name => "Ladder";

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Interract()
    {
        player.transform.position = newPosition.transform.position;
    }

    public bool PlayerIsInTrigger()
    {
        List<Collider> colliders = Physics.OverlapBox(gameObject.transform.position, gameObject.transform.localScale).ToList();
        return colliders.Contains(player.GetComponent<Collider>());
    }
}
