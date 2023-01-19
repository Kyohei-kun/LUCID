using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CS_TriggerParatonnerre : MonoBehaviour, I_Interact
{
    GameObject player;
    [SerializeField] CS_Paratonnerre paratonnerre;

    public string UI_Name => "lightning rod";

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Interract()
    {
        if (paratonnerre.IsWork)
            paratonnerre.StopWork();
        else
            paratonnerre.StartWork();
    }

    public bool PlayerIsInTrigger()
    {
        List<Collider> colliders = Physics.OverlapBox(gameObject.transform.position, gameObject.transform.localScale).ToList();
        return colliders.Contains(player.GetComponent<Collider>());
    }
}
