using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Wyrve : MonoBehaviour
{
    //Debug 
    [ShowNonSerializedField] private string nameCurrentState;

    private CS_IA_Manager manager;
    private CS_I_State currentState;
    private CS_I_State lastState;
    private float distancePlayer;
    private Vector3 startPosition;

    [BoxGroup("IA")] [SerializeField] float radiusZone;
    [BoxGroup("IA")] [SerializeField] float disctanceTriggerPlayer;

    [BoxGroup("Mouvements")] [SerializeField] float smoothReturnMove;
    [BoxGroup("Mouvements")] [SerializeField] float speed;
    [BoxGroup("Mouvements")] [SerializeField] float amplitude;
    [BoxGroup("Mouvements")] [SerializeField] float frequency;

    public float DistancePlayer { get => distancePlayer; set => distancePlayer = value; }
    public float SmoothReturnMove { get => smoothReturnMove; set => smoothReturnMove = value; }
    public float Speed { get => speed; set => speed = value; }
    public CS_IA_Manager Manager { get => manager; set => manager = value; }
    public Vector3 StartPosition { get => startPosition; set => startPosition = value; }
    public float Amplitude { get => amplitude; set => amplitude = value; }
    public float Frequency { get => frequency; set => frequency = value; }
    public float RadiusZone { get => radiusZone; set => radiusZone = value; }
    public float DisctanceTriggerPlayer { get => disctanceTriggerPlayer; set => disctanceTriggerPlayer = value; }

    public delegate void DelegateGizmo();
    public List<DelegateGizmo> gizmoDelegate;

    private void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("IA_Manager").GetComponent<CS_IA_Manager>();
        Manager.Subscribe(this);
        currentState = new CS_Patrouille();
        StartPosition = transform.position;
        gizmoDelegate = new List<DelegateGizmo>();
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

    private void OnDrawGizmosSelected()
    {
        if (gizmoDelegate != null && gizmoDelegate.Count > 0)
        {
            foreach (var item in gizmoDelegate)
            {
                item();
            }
        }
    }

    public void UpdatePlayerDistance()
    {
        distancePlayer = Vector3.Distance(transform.position, manager.GetPlayerGameObject().transform.position);
    }
}
