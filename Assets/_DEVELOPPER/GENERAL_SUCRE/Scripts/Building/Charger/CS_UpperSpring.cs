using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_UpperSpring : MonoBehaviour
{
    Rigidbody _rigidbody;
    [SerializeField] float playerMass;
    [SerializeField] Transform underSpring;
    [SerializeField] CS_Charger charger;
    bool canCharge = true;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _rigidbody.AddForce(Vector3.down * Time.deltaTime * playerMass);
            if (Vector3.Distance(gameObject.transform.position, underSpring.position) < 0.5f)
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
