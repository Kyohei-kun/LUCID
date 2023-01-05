using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_TestSpeedRotation : MonoBehaviour
{
    [SerializeField] GameObject island;
    [SerializeField] Rigidbody R_Island;
    [SerializeField] Rigidbody _rigidbody;

    float R;
    Vector3 velocity;
    Vector3 tangeante;

    private void Update()
    {
        //float rotationAmount = R_Island.angularVelocity.y * 57.29577951308232f * Time.deltaTime;

        //Quaternion localAngleAxis = Quaternion.AngleAxis(rotationAmount, Vector3.up);
        //_rigidbody.position = (localAngleAxis * (_rigidbody.position - R_Island.position)) + R_Island.position;

        R = Vector3.ProjectOnPlane((gameObject.transform.position - island.transform.position), Vector3.up).magnitude;
        velocity = (R_Island.angularVelocity * R);


        tangeante = Quaternion.Euler(Vector3.up * (-90)) * Vector3.ProjectOnPlane(island.transform.position - gameObject.transform.position, Vector3.up);
        tangeante = tangeante.normalized;
        _rigidbody.velocity = tangeante.normalized * 1;//velocity.magnitude;
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (Vector3.ProjectOnPlane(island.transform.position - gameObject.transform.position, Vector3.up)), Color.red);
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + tangeante * velocity.magnitude);
    }
}