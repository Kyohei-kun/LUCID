using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Wyrve : MonoBehaviour
{
    //Debug 
    [ShowNonSerializedField] private string nameCurrentState;
    bool drawGizmos = false;
    bool notDrawGizmos => !drawGizmos;
    bool showTrail = false;
    bool notShowTrail => !showTrail;
    TrailRenderer trail;

    private CS_IA_Manager manager;
    private CS_I_State currentState;
    private CS_I_State lastState;
    private float distancePlayer = 1000000;
    private Vector3 startPosition;

    public delegate void DelegateGizmo();
    public List<DelegateGizmo> gizmoDelegate;

    [HorizontalLine(color: EColor.Blue)]
    [BoxGroup("IA")] [SerializeField] float radiusZone;
    [BoxGroup("IA")] [SerializeField] float disctanceTriggerPlayer;


    [HorizontalLine(color: EColor.Blue)]
    [BoxGroup("Patrouille")] [SerializeField] float smoothReturnMove;
    [BoxGroup("Patrouille")] [SerializeField] float speed;
    [BoxGroup("Patrouille")] [SerializeField] float amplitude;
    [BoxGroup("Patrouille")] [SerializeField] float frequency;

    [HorizontalLine(color: EColor.Blue)]
    [Range(0, 1)]
    [BoxGroup("PrepareAttack")] [SerializeField] float smoothAdjustement;
    [BoxGroup("PrepareAttack")] [SerializeField] float angleRise;

    [HorizontalLine(color: EColor.Blue)]
    [BoxGroup("Animation")] [SerializeField] Animator animator;


    #region GET|SET
    public float DistancePlayer { get => distancePlayer; set => distancePlayer = value; }
    public float SmoothReturnMove { get => smoothReturnMove; set => smoothReturnMove = value; }
    public float Speed { get => speed; set => speed = value; }
    public CS_IA_Manager Manager { get => manager; set => manager = value; }
    public Vector3 StartPosition { get => startPosition; set => startPosition = value; }
    public float Amplitude { get => amplitude; set => amplitude = value; }
    public float Frequency { get => frequency; set => frequency = value; }
    public float RadiusZone { get => radiusZone; set => radiusZone = value; }
    public float DisctanceTriggerPlayer { get => disctanceTriggerPlayer; set => disctanceTriggerPlayer = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public float SmoothAdjustement { get => smoothAdjustement; set => smoothAdjustement = value; }
    public float AngleRise { get => angleRise; set => angleRise = value; }
    #endregion

    private void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("IA_Manager").GetComponent<CS_IA_Manager>();
        Manager.Subscribe(this);
        currentState = new CS_Patrouille();
        StartPosition = transform.position;
        gizmoDelegate = new List<DelegateGizmo>();
        trail = GetComponentInChildren<TrailRenderer>();
        trail.enabled = showTrail;
    }

    private void Update()
    {
        nameCurrentState = currentState.ToString();  //Debug currentState name

        if (currentState != lastState)
        {
            currentState.State_Start(this); //■
        }

        currentState.State_Update(); //■

        CS_I_State newState = currentState.State_Pass(); //■
        if (newState != null)
        {
            currentState.State_Finish(); //■
            lastState = currentState;
            currentState = newState;
        }
        else
        {
            lastState = currentState;
        }
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            if (gizmoDelegate != null && gizmoDelegate.Count > 0)
            {
                foreach (var item in gizmoDelegate)
                {
                    item();
                }
            }
        }
    }

    public void UpdatePlayerDistance()
    {
        distancePlayer = Vector3.Distance(transform.position, manager.GetPlayerGameObject().transform.position);
    }


    [Button(enabledMode: EButtonEnableMode.Playmode)]
    private void RewriteDistancePlayer() { distancePlayer = 0; }

    //Gizmo Button
    [ShowIf("notDrawGizmos")]
    [Button()]
    private void Activate_Gizmos() { drawGizmos = true; }

    [ShowIf("drawGizmos")]
    [Button()]
    private void Desactivate_Gizmos() { drawGizmos = false; }

    //Trail Button
    [ShowIf("notShowTrail")]
    [Button()]
    private void Activate_Trail() { trail.enabled = showTrail = true; }

    [ShowIf("showTrail")]
    [Button()]
    private void Desactivate_Trail() { trail.enabled = showTrail = false; }
}
