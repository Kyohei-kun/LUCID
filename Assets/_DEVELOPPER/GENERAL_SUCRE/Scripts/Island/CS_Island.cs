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
    [Header("Refences")]
    GameObject player;
    ThirdPersonController thirdPersonController;
    PlayerInput playerInput;
    Rigidbody _rigidbody;

    [Space(10)]
    [Header("Controls Values")]
    [Space(5)]
    //[Tooltip("X = Forward | Y = UP | Z = RightLeft")]
    [SerializeField] AnimationCurve speedForwardCurve;
    [SerializeField] AnimationCurve speedRotationCurve;
    [SerializeField] AnimationCurve speedUpCurve;

    [SerializeField] AnimationCurve slowDamperCurve;

    [SerializeField] private float speedMaxRotation;
    [SerializeField] private float speedMaxBoost;
    [SerializeField] private float speedMaxAltitude;

    [Space(10)]
    [Header("Threshold Controls")]
    [Space(5)]
    [SerializeField] float thresholdRotation;
    [SerializeField] float thresholdBoost;


    float timerBeginRotation;
    float timerBeginAltittude;
    float timerBeginBoost;

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


    [Space(10)]
    [Header("Camera")]
    [Space(5)]
    [SerializeField] private GameObject CM_Camera;
    [SerializeField] private float thresholdLookCamera;
    [SerializeField] CinemachineBrain CM_Brain;

    [Space(10)]
    [Header("UI")]
    [Space(5)]
    [SerializeField] TextMeshProUGUI UI_angleRot;

    [Space(10)]
    [Header("Controls Values")]
    [Space(5)]
    [SerializeField] Animator animatorWings;

    [Space(10)]
    [Header("Barre")]
    [Space(5)]
    [SerializeField] GameObject barre;

    [Space(10)]
    [Header("Altimètre")]
    [Space(5)]
    [SerializeField] Transform controlCursor;
    [SerializeField] Transform currentCursor;
    bool changingAltitude;
    bool LastChangeAltitude;

    [Space(10)]
    [Header("FX")]
    [Space(5)]
    [SerializeField] GameObject windFx;

    [Space(10)]
    [Header("FeedBack Movement")]
    [Space(5)]

    [SerializeField] float angleRotationFeedBack;
    [SerializeField] float anglePitchClamp;
    [SerializeField] AnimationCurve anglePitchCurve;
    [SerializeField] AnimationCurve anglePitchCurveRecovery;
    [SerializeField] float anglePitchSpeed;
    [SerializeField] AnimationCurve _angleByDistance;

    float startAltitude;

    public bool IsDriving { get => isDriving; set => isDriving = value; }
    public float CurrentBoost { get => currentBoost; set => currentBoost = value; }

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
        if (rightLeft == 0 && Mathf.Abs(currentAngleDirection) < thresholdRotation && currentAngleDirection == lastAngleDirection)
        {
            currentAngleDirection = 0;

        }

        if (forwardBackward == 0 && Mathf.Abs(CurrentBoost) < thresholdBoost && CurrentBoost == lastBoost)
        {
            CurrentBoost = 0;
        }

        currentAltitude = transform.position.y;

        if (rightLeft > 0.5f)
        {
            //currentAngleDirection += 0.01f;
            currentAngleDirection += speedRotationCurve.Evaluate(timerBeginRotation);
        }
        else if (rightLeft < -0.5f)
        {
            currentAngleDirection -= speedRotationCurve.Evaluate(timerBeginRotation);
        }
        currentAngleDirection = Mathf.Clamp(currentAngleDirection, -1f, 1f);

        if (forwardBackward > 0.5f)
        {
            CurrentBoost += speedForwardCurve.Evaluate(timerBeginBoost);
        }
        else if (forwardBackward < -0.5f)
        {
            CurrentBoost -= speedForwardCurve.Evaluate(timerBeginBoost);
        }

        CurrentBoost = Mathf.Clamp(CurrentBoost, -1, 1);

        windFx.SetActive(CurrentBoost > 0);

        UptadeAnimationWings();

        transform.Rotate(Vector3.up * currentAngleDirection * Time.deltaTime * speedMaxRotation);
        transform.Translate(-transform.forward * CurrentBoost * Time.deltaTime * speedMaxBoost, Space.World);

        float slowMultiplicateur = 1;

        float deltaAltitude = targetAltitude - currentAltitude;
        if (deltaAltitude > 5)
        {
            //transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * speedMaxAltitude, transform.position.z);
            if (timerBeginAltittude != 0)
            {
                slowMultiplicateur = slowDamperCurve.Evaluate(deltaAltitude);
            }
            transform.Translate(slowMultiplicateur * Vector3.up * Time.deltaTime * speedMaxAltitude * speedUpCurve.Evaluate(timerBeginAltittude));
        }
        else if (deltaAltitude < -5)
        {
            if (timerBeginAltittude != 0)
            {
                slowMultiplicateur = slowDamperCurve.Evaluate(-deltaAltitude);
            }
            //transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * -speedMaxAltitude, transform.position.z);
            transform.Translate(slowMultiplicateur * -Vector3.up * Time.deltaTime * speedMaxAltitude * speedUpCurve.Evaluate(timerBeginAltittude));
        }
        else
        {
            timerBeginAltittude = 0;
        }

        UpdateBarreRotation();
        UpdateAltimetre();

        UpdateRotationFeedBack();

        lastAngleDirection = currentAngleDirection;
        lastBoost = CurrentBoost;
        LastChangeAltitude = changingAltitude;

        timerBeginRotation += Time.deltaTime;
        timerBeginAltittude += Time.deltaTime;
        timerBeginBoost += Time.deltaTime;
    }

    private void UptadeAnimationWings()
    {
        animatorWings.speed = Mathf.Abs(CurrentBoost);
    }

    private void UpdateAltimetre()
    {
        float tempAlt = currentAltitude;
        currentCursor.localRotation = Quaternion.Euler(tempAlt.Remap(20000, -20000, 0, 80), 90, 180);

        float tempTargetAltitude = targetAltitude;
        controlCursor.localRotation = Quaternion.Euler(tempTargetAltitude.Remap(20000, -20000, 0, 80), 90, 0);

        if (Mathf.Abs(currentAltitude - targetAltitude) > 5 && changingAltitude == false)
        {
            changingAltitude = true;
        }
        else if (Mathf.Abs(currentAltitude - targetAltitude) < 5)
        {
            changingAltitude = false;
        }
    }

    private float GetPitchRotationFeedBack(Quaternion quat)
    {
        float xRot = quat.eulerAngles.x;
        float result = 0;
        float deltaAlti = targetAltitude - currentAltitude;

        if (changingAltitude == true && LastChangeAltitude == false)
        {
            startAltitude = transform.position.y;
        }

        if (changingAltitude)
        {
            float temp = transform.position.y.Remap(startAltitude, targetAltitude, 0, 1);
            result = _angleByDistance.Evaluate(transform.position.y.Remap(startAltitude, targetAltitude, 0, 1));

            if (deltaAlti < 0)
            {
                result = -result;
            }
        }

        Quaternion targetQuaternion = Quaternion.Euler(result, quat.eulerAngles.y, quat.eulerAngles.z);

        result = Quaternion.Lerp(quat, targetQuaternion, 0.01f).eulerAngles.x;

        return result;
    }


    private void UpdateRotationFeedBack()
    {
        //Debug.Log(transform.rotation.eulerAngles.y.Remap(0,360,-180,180));
        Vector3 rot = transform.rotation.eulerAngles;

        float t = Mathf.Clamp(currentAngleDirection, -1f, 1f).Remap(-1f, 1f, 0, 1);
        //Debug.Log(t);

        if (currentAngleDirection > 0)
        {
            t = t.Remap(0.5f, 1, 0, 1);
            //Debug.Log("T = " + t + " degré " + Mathf.Lerp(0, 15, t));
            rot.z = Mathf.Lerp(0, angleRotationFeedBack, t);
        }
        if (currentAngleDirection < 0)
        {
            t = t.Remap(0, 0.5f, 1, 0);
            //Debug.Log("T = " + t + " degré " + Mathf.Lerp(360, 345, t));
            rot.z = Mathf.Lerp(360, 360 - angleRotationFeedBack, t);
        }
        rot.x = GetPitchRotationFeedBack(Quaternion.Euler(rot));
        transform.localRotation = Quaternion.Euler(rot);
    }

    private void UpdateBarreRotation()
    {
        UI_angleRot.text = currentAngleDirection.ToString();
        float tempAngleDirection = currentAngleDirection;
        tempAngleDirection += 1;
        float angleBarre = Mathf.Lerp(-450, 450, tempAngleDirection.Remap(0, 2, 0, 1));
        barre.transform.localRotation = Quaternion.Euler(0, 0, angleBarre);
    }

    #region Inputs
    public void Move(CallbackContext value)
    {
        Vector2 vector = value.ReadValue<Vector2>();
        rightLeft = vector.x;
        forwardBackward = vector.y;

        timerBeginRotation = 0;
        timerBeginBoost = 0;
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
    #endregion

    #region Trigger
    private void OnTriggerEnter(Collider other) //Attach player into island
    {
        if (other.tag == "Player")
        {
            other.transform.parent = gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other) //Detach player of island
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
    #endregion
}