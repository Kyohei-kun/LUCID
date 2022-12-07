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
    float forwardBackward;
    float upDown;

    float currentAngleDirection;
    float currentBoost;
    float currentAltitude;

    float targetAltitude;

    [SerializeField] private float speedRotation;
    [SerializeField] private float speedBoost;
    [SerializeField] private float speedAltitude;

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
        currentAltitude = transform.position.y;

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

        if (forwardBackward > 0.5f)
        {
            currentBoost += 0.01f;
        }
        else if (forwardBackward < -0.5f)
        {
            currentBoost -= 0.01f;
        }

        currentBoost = Mathf.Clamp(currentBoost, -1, 1);

        transform.Rotate(Vector3.up * currentAngleDirection * Time.deltaTime * speedRotation);
        transform.position = _rigidbody.position - transform.forward * currentBoost * Time.deltaTime * speedBoost;

        float deltaAltitude = targetAltitude - currentAltitude;
        if (deltaAltitude > 10)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * speedAltitude, transform.position.z);
        }
        else if(deltaAltitude < -10)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * -speedAltitude, transform.position.z);
        }

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
        forwardBackward = vector.y;
    }

    public void Altitude(CallbackContext value)
    {
        upDown = value.ReadValue<Vector2>().y;
        targetAltitude += upDown / 20;
    }
}
