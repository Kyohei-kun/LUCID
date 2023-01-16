using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Lamp : CS_Item
{
    [SerializeField] Material sourceMaterial;
    [SerializeField] GameObject visualGameObject;

    Material currentMaterial;

    [Space(5)]
    CS_Battery battery;
    [SerializeField] Transform socket;
    [SerializeField] Vector3 decalSmallBattery;
    [SerializeField] Vector3 decalLargeBattery;

    bool canWork;

    private new void Start()
    {
        base.Start();
        
        visualGameObject.GetComponent<Renderer>().materials[1] = new Material(sourceMaterial);
        currentMaterial = visualGameObject.GetComponent<Renderer>().materials[1];
    }

    private void Update()
    {
        if(canWork)
        {
            if(battery.CurrentEnergy>1)
            {
                battery.UseEnergy(1 * Time.deltaTime);
            }
            else
            {
                currentMaterial.SetFloat("_OnOff", 0);
                canWork = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CS_Battery>() != null && battery == null && other.GetComponent<CS_Battery>().IsInHand == false)
        {
            battery = other.GetComponent<CS_Battery>();
            battery.gameObject.transform.position = socket.position + (battery.SizeItem == SizeItem.Small ? decalSmallBattery : decalLargeBattery);
            battery.transform.localEulerAngles = Vector3.zero;
            battery.GetComponent<Rigidbody>().isKinematic = true;
            currentMaterial.SetFloat("_OnOff", 1);
            canBeTakable = false;
            canWork = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CS_Battery>() != null && other.GetComponent<CS_Battery>() == battery)
        {
            battery.GetComponent<Rigidbody>().isKinematic = true;
            battery = null;
            currentMaterial.SetFloat("_OnOff", 0);
            canBeTakable = true;
            canWork = false;
        }
    }
}
