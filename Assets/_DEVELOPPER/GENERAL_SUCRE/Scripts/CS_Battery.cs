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

    private void Start()
    {
        fillObject.GetComponent<Renderer>().material = new Material(source_MatFill);
        currentMatFill = fillObject.GetComponent<Renderer>().material;
        UpdateFill();
    }

    public float Charge(float energy)
    {
        float deltaEnergy = maxEnergy - currentEnergy;
        float reste = energy - deltaEnergy;

        currentEnergy += energy;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);

        UpdateFill();

        if (reste > 0)
            return reste;
        else
            return 0;
    }

    private void UpdateFill()
    {
        float value = currentEnergy.Remap(0, maxEnergy, 0, 1);
        currentMatFill.SetFloat("_Fill", value);
        //fillObject.GetComponent<Renderer>().material
    }
}
