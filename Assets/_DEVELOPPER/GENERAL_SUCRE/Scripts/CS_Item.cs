using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SizeItem
{
    Small,
    Large
}

public class CS_Item : MonoBehaviour
{
    [SerializeField] private SizeItem sizeItem;
    private bool isInHand;
    protected Rigidbody _rigidbody;

    public SizeItem SizeItem { get => sizeItem; set => sizeItem = value; }
    public bool IsInHand { get => isInHand; set => isInHand = value; }

    protected void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Taked(Transform socket)
    {
        IsInHand = true;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        transform.parent = socket;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }

    public void Dropped()
    {
        IsInHand = false;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        if (transform.parent.parent.parent.tag == "Island")
        {
            transform.parent = transform.parent.parent.parent;
        }
        else
            transform.parent = null;
    }
}
