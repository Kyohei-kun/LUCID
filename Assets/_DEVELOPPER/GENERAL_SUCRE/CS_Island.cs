using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class CS_Island : MonoBehaviour
{
    GameObject player;
    Rigidbody _rigidbody;

    float rightLeft;
    float currentAngleDirection;
    [SerializeField] private float speedRotation;

    [Space(10)]
    [SerializeField] TextMeshProUGUI UI_angleRot;

    private void Start()
    {
        CS_InputManager.Initialisation();
        _rigidbody = GetComponent<Rigidbody>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            CS_InputManager.ChangePiloted(PlayerPilote.Island);
            Debug.Log("Pilote Island");
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CS_InputManager.ChangePiloted(PlayerPilote.None);
            Debug.Log("Pilote Player");
        }

        if (rightLeft > 0.5f)
        {
            currentAngleDirection += 0.01f;
        }
        else if (rightLeft < -0.5f)
        {
            currentAngleDirection -= 0.01f;
        }
        currentAngleDirection = Mathf.Clamp(currentAngleDirection, -1f, 1f);

        //_rigidbody.rotation = _rigidbody.rotation * Quaternion.Euler(new Vector3(0, currentAngleDirection * Time.deltaTime * speedRotation, 0));

        _rigidbody.angularVelocity = Vector3.up * currentAngleDirection /2;
        //player.GetComponent<CharacterController>().SimpleMove(_rigidbody.angularVelocity * Vector3.ProjectOnPlane((player.transform.position - gameObject.transform.position), Vector3.up).magnitude);

        UI_Debug();
    }

    private void UI_Debug()
    {
        UI_angleRot.text = currentAngleDirection.ToString();
    }

    public void Move(CallbackContext value)
    {
        Vector2 vector = value.ReadValue<Vector2>();
        rightLeft = vector.x;
    }
}
