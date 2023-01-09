using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CS_Battery : CS_Item
{
    [SerializeField] float currentEnergy;
    [SerializeField] float maxEnergy;

    [Space(10)] [Header("Fill")]
    [SerializeField] GameObject fillObject;
    [SerializeField] Material source_MatFill;
    Material currentMatFill;

    public float CurrentEnergy { get => currentEnergy; set => currentEnergy = value; }

    protected new void Start()
    {
        base.Start();
        //currentMatFill = new Material(source_MatFill);
        fillObject.GetComponent<Renderer>().material = new Material(source_MatFill);
        currentMatFill = fillObject.GetComponent<Renderer>().material;
        UpdateFill();
    }

    public float Charge(float energy)
    {
        float deltaEnergy = maxEnergy - currentEnergy;

        currentEnergy += energy;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);

        UpdateFill();

        if (deltaEnergy > energy)
        {
            return energy;
        }
        else if(deltaEnergy == energy)
        {
            return 0;
        }
        else
        {
            return deltaEnergy;
        }
    }

    private void UpdateFill()
    {
        float value = currentEnergy.Remap(0, maxEnergy, 0, 1);
        currentMatFill.SetFloat("_Fill", value);
        //fillObject.GetComponent<Renderer>().material
    }
}
