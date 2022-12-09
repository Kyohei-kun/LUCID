using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

static public class CS_PilotManager 
{
    static public PlayerPilote playerPilote;
    static public GameObject player;
    static public PlayerInput playerInput;

    static public GameObject island;
    static public PlayerInput playerInputIsland;

    static public void Initialisation()
    {
        playerPilote = PlayerPilote.None;
        player = GameObject.FindGameObjectWithTag("Player");
        playerInput = player.GetComponent<PlayerInput>();

        island = GameObject.FindGameObjectWithTag("Island");
        playerInputIsland = island.GetComponent<PlayerInput>();

        playerInputIsland.enabled = false;
        playerInput.enabled = false;

        playerInput.enabled = true;
    }

    static public void ChangePiloted(PlayerPilote newState)
    {
        if (newState == PlayerPilote.Island)
        {
            playerInput.enabled = false;
            playerInputIsland.enabled = false;

            playerInputIsland.enabled = true;
        }
        else if (newState == PlayerPilote.None)
        {
            playerInput.enabled = false;
            playerInputIsland.enabled = false;
            
            playerInput.enabled = true;
        }
        

        playerPilote = newState;
    }
}

public enum PlayerPilote
{
    None,
    Island
}
