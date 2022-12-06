using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class CS_Island : MonoBehaviour
{
    GameObject player;
    Rigidbody _rigidbody;

    float rightLeft;
    [SerializeField] private float speedRotation;

    private void Start()
    {
        CS_InputManager.Initialisation();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            CS_InputManager.ChangePiloted(PlayerPilote.Island);
        }
        else if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CS_InputManager.ChangePiloted(PlayerPilote.None);
        }

        _rigidbody.rotation = _rigidbody.rotation * Quaternion.Euler(new Vector3(0,rightLeft * Time.deltaTime * speedRotation,0));


    }

    public void Move(CallbackContext value)
    {
        Vector2 vector = value.ReadValue<Vector2>();
        rightLeft = vector.x;
    }
}
