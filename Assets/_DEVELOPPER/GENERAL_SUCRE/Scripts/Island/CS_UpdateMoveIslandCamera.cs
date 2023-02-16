using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_UpdateMoveIslandCamera : MonoBehaviour
{
    [SerializeField] float decal;
    [SerializeField] CS_Island island;

    Vector3 initPosition;
    Vector3 forwardPosition;
    Vector3 backwardPosition;

    float alpha;

    private void Start()
    {
        initPosition = transform.localPosition;
        forwardPosition = initPosition + (transform.forward * decal);
        backwardPosition = initPosition + (-transform.forward * decal);
    }

    private void Update()
    {
        alpha = Mathf.Clamp(island.CurrentBoost, -0.2f, 0.2f);
        alpha = alpha.Remap(-0.2f, 0.2f, 1, 0);
        transform.localPosition = Vector3.Lerp(backwardPosition, forwardPosition, alpha);
    }
}
