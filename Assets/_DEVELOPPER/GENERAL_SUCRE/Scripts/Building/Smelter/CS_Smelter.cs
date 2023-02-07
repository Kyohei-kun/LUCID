using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Smelter : CS_ElectricBuilding
{
    [SerializeField] Animator _animator;
    [SerializeField] GameObject prefabMetal;
    [SerializeField] Transform socket;
    Transform itemParent;
    [Space(10)]
    [SerializeField] int scrapByMetal;
    [SerializeField] float energyCost;
    [SerializeField] float timeSmelting;

    List<CS_Scrap> bufferScraps;
    float currentTimer;
    float currentCostUsed;
    float targetCost;
    float deltaCost;


    bool haveScrap;
    bool haveEnergy;

    int nbScrapCharged;


    private void Start()
    {
        bufferScraps = new List<CS_Scrap>();
        itemParent = GameObject.FindGameObjectWithTag("ItemsParent").transform;
    }

    private void Update()
    {
        float futureCurrentTimer = Mathf.Clamp(currentTimer + Time.deltaTime, 0, timeSmelting);
        targetCost = futureCurrentTimer.Remap(0, timeSmelting, 0, energyCost);
        deltaCost = targetCost - currentCostUsed;

        CheckRessources();

        if (haveScrap && haveEnergy)
        {
            currentTimer += Time.deltaTime;
            battery.UseEnergy(deltaCost);
            currentCostUsed += deltaCost;

            if (currentTimer >= timeSmelting)
            {
                GameObject temp = Instantiate(prefabMetal);
                temp.transform.position = socket.position;
                temp.transform.parent = itemParent;
                currentTimer = 0;
                nbScrapCharged = 0;
                currentCostUsed = 0;
                _animator.SetInteger("NbScraps", nbScrapCharged);
            }
        }
    }

    private void CheckRessources()
    {
        if (nbScrapCharged >= scrapByMetal)
        {
            haveScrap = true;
        }
        else
        {
            if (bufferScraps.Count > 0)
            {
                Destroy(bufferScraps[0].gameObject);
                bufferScraps.RemoveAt(0);
                nbScrapCharged++;
                _animator.SetInteger("NbScraps", nbScrapCharged);
            }
            haveScrap = false;
        }

        
        if (battery != null && battery.HaveEnoughEnergy(deltaCost))
        {
            haveEnergy = true;
        }
        else
        {
            haveEnergy = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CS_Scrap scrapTemp = other.GetComponent<CS_Scrap>();

        if (scrapTemp != null && scrapTemp.IsInHand == false)
        {
            if (nbScrapCharged < scrapByMetal)
            {
                Destroy(other.gameObject);
                nbScrapCharged++;
                _animator.SetInteger("NbScraps", nbScrapCharged);
            }
            else
            {
                bufferScraps.Add(other.GetComponent<CS_Scrap>());
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CS_Scrap>() != null)
        {
            bufferScraps.Remove(other.GetComponent<CS_Scrap>());
        }
    }
}