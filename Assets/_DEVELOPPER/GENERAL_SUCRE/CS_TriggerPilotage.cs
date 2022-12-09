using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CS_TriggerPilotage : MonoBehaviour, I_Interact
{
    [SerializeField] GameObject cameraPilote;
    [SerializeField] GameObject cameraPlayer;

    [SerializeField] CS_Island island;
    [SerializeField] CS_UIManager UI_Manager;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public string UI_Name { get => "driving";}

    public void Interract()
    {
        island.ResetDecalLook();
        CS_PilotManager.ChangePiloted(PlayerPilote.Island);
        island.IsDriving = true;
        SwitchCamera();
        UI_Manager.UnDrawInteractUI();
    }

    public bool PlayerIsInTrigger()
    {
        List<Collider> colliders = Physics.OverlapBox(gameObject.transform.position, gameObject.transform.localScale).ToList();
        return colliders.Contains(player.GetComponent<Collider>());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && island.IsDriving)
        {
            island.IsDriving = false;
            CS_PilotManager.ChangePiloted(PlayerPilote.None);
            SwitchCamera();
            UI_Manager.DrawInteractUI(UI_Name);
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            CS_PilotManager.ChangePiloted(PlayerPilote.Island);
            SwitchCamera();
            Debug.Log("Pilote Island");
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CS_PilotManager.ChangePiloted(PlayerPilote.None);
            SwitchCamera();
            Debug.Log("Pilote Player");
        }
    }

    private void SwitchCamera()
    {
        if(island.IsDriving)
        {
            cameraPilote.SetActive(true);
            cameraPlayer.SetActive(false);
        }
        else
        {
            cameraPilote.SetActive(false);
            cameraPlayer.SetActive(true);
        }
    }
}
