using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_UpperSpring : MonoBehaviour
{
    Rigidbody _rigidbody;
    [SerializeField] float playerMass;
    [SerializeField] Transform underSpring;
    [SerializeField] Transform middleBone;
    [SerializeField] CS_Charger charger;
    [SerializeField] float distanceThresholdEnergy;
    bool canCharge = true;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        middleBone.position = new Vector3(middleBone.position.x, underSpring.position.y + (Vector3.Distance(underSpring.position, gameObject.transform.position) /2), middleBone.position.z);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _rigidbody.AddForce(Vector3.down * Time.deltaTime * playerMass);
            if (Vector3.Distance(gameObject.transform.position, underSpring.position) < distanceThresholdEnergy)
            {
                if (canCharge)
                {
                    canCharge = false;
                    charger.StartWork();
                }
            }
            else
            {
                canCharge = true;
            }
        }
    }
}
