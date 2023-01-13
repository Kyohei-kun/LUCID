using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Transfert : CS_ElectricBuilding
{
    [SerializeField] float EU_By_Second;

    [Header("Animations")]
    [Space(10)]
    [SerializeField] Animator animator;

    [Header("Materials Feedbacks")]
    [Space(10)]
    [SerializeField] Renderer visuelRender;
    [SerializeField] Material sourceMaterial;

    private CS_Battery receiverBattery;
    Coroutine co_Transfert;

    private void Start()
    {
        visuelRender.materials[1] = new Material(sourceMaterial);
    }

    public void SetBattery(CS_Battery bat, bool isReceiver)
    {
        if (isReceiver)
        {
            receiverBattery = bat;
        }
        else
        {
            battery = bat;
        }

        CheckIfCanWork();
    }

    private void CheckIfCanWork()
    {
        if (battery != null && receiverBattery != null)
        {
            StartWork();
        }
        else
        {
            StopWork();
        }
    }

    public new void StartWork()
    {
        co_Transfert = StartCoroutine(TransfertEnergy());
        animator.SetBool("IfOn", true);
        visuelRender.materials[1].SetFloat("_OnOff", 1);
    }

    public new void StopWork()
    {
        if(co_Transfert != null)
        StopCoroutine(co_Transfert);
        animator.SetBool("IfOn", false);
        visuelRender.materials[1].SetFloat("_OnOff", 0);
    }

    IEnumerator TransfertEnergy()
    {
        while (true)
        {
            if (battery.CurrentEnergy == 0 || receiverBattery.CurrentEnergy == receiverBattery.MaxEnergy)
                StopWork();

            float valueTransfert = Time.deltaTime.Remap(0, 1, 0, EU_By_Second);
            if(battery.CurrentEnergy >= valueTransfert)
            battery.Charge(-receiverBattery.Charge(valueTransfert));
            else
            {
                battery.Charge(-receiverBattery.Charge(battery.CurrentEnergy));
            }

            yield return 0;
        }
    }
}
