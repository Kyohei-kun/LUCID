using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using static UnityEngine.InputSystem.InputAction;

public class CS_Island : MonoBehaviour
{
    GameObject player;
    ThirdPersonController thirdPersonController;
    PlayerInput playerInput;
    Rigidbody _rigidbody;
    private bool isDriving = false;

    Vector2 initRotation;
    float decalCinemachineTargetPitch = 0;
    float decalCinemachineTargetYaw = 0;

    float rightLeft;
    float forwardBackward;
    float upDown;

    float currentAngleDirection;
    float lastAngleDirection;

    float currentBoost;
    float lastBoost;
    float currentAltitude;

    float targetAltitude;

    [SerializeField] private float speedRotation;
    [SerializeField] private float speedBoost;
    [SerializeField] private float speedAltitude;

    [Space(5)]
    [SerializeField] private GameObject CM_Camera;
    [SerializeField] private float thresholdLookCamera;

    [Space(10)]
    [SerializeField] TextMeshProUGUI UI_angleRot;

    [SerializeField] CinemachineBrain CM_Brain;

    public bool IsDriving { get => isDriving; set => isDriving = value; }
    
    private void Start()
    {
        CS_PilotManager.Initialisation();
        _rigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        thirdPersonController = player.GetComponent<ThirdPersonController>();
        playerInput = GetComponent<PlayerInput>();
        initRotation = new Vector2(CM_Camera.transform.localRotation.eulerAngles.x, CM_Camera.transform.localRotation.eulerAngles.y);
    }

    private void Update()
    {
        if (rightLeft == 0 && Mathf.Abs(currentAngleDirection) < 0.2f && currentAngleDirection == lastAngleDirection)
        {
            currentAngleDirection = 0;
        }

        if (forwardBackward == 0 && Mathf.Abs(currentBoost) < 0.2f && currentBoost == lastBoost)
        {
            currentBoost = 0;
        }

        currentAltitude = transform.position.y;

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
        else if (deltaAltitude < -10)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * -speedAltitude, transform.position.z);
        }

        UI_Debug();

        lastAngleDirection = currentAngleDirection;
        lastBoost = currentBoost;
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

    public void ResetDecalLook()
    {
        decalCinemachineTargetYaw = 0;
        decalCinemachineTargetPitch = 0;
    }

    public void Look(CallbackContext value)
    {
        if (CM_Brain.IsBlending == false && isDriving)
        {
            Vector2 direction = value.ReadValue<Vector2>();

            float deltaTimeMultiplier = playerInput.currentControlScheme == "KeyboardMouse" ? 1.0f : Time.deltaTime;

            // if there is an input and camera position is not fixed
            if (direction.sqrMagnitude >= thresholdLookCamera)
            {
                decalCinemachineTargetPitch += direction.y * deltaTimeMultiplier;
                decalCinemachineTargetYaw += direction.x * deltaTimeMultiplier;
            }
            
            decalCinemachineTargetPitch = Mathf.Clamp(decalCinemachineTargetPitch, -50, 20);
            decalCinemachineTargetYaw = Mathf.Clamp(decalCinemachineTargetYaw, -90, 90);

            CM_Camera.transform.localRotation = Quaternion.Euler(initRotation.x + decalCinemachineTargetPitch, initRotation.y + decalCinemachineTargetYaw, 0.0f);
        }
    }
}