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
    protected E_Item enumItem;

    [SerializeField] protected bool isInHand;
    [SerializeField] protected bool canBeTakable = true;
    protected Rigidbody _rigidbody;

    [SerializeField] protected Vector3 defaultRotation;

    public SizeItem SizeItem { get => sizeItem; set => sizeItem = value; }
    public bool IsInHand { get => isInHand; set => isInHand = value; }
    public bool CanBeTakable { get => canBeTakable; set => canBeTakable = value; }

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

    public E_Item ToEnum()
    {
        return enumItem;
    }
   
}
