using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            ChangePlayerTransparency(0.5f);
        }
        else if (newState == PlayerPilote.None)
        {
            playerInput.enabled = false;
            playerInputIsland.enabled = false;
            
            playerInput.enabled = true;
            ChangePlayerTransparency(1);
        }

        playerPilote = newState;
    }

    static private void ChangePlayerTransparency(float transpancyValue)
    {
        foreach (var renderer in player.GetComponentsInChildren<Renderer>().ToList())
        {
            foreach (var item in renderer.materials.ToList())
            {
                Color color = item.color;
                color.a = transpancyValue;
                item.color = color;
            }
        }
    }
}

public enum PlayerPilote
{
    None,
    Island
}