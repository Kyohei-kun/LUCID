using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CS_GrandeLigne : CS_ElectricBuilding
{
    [SerializeField] Animator animator;
    [SerializeField] float chanceSpawn;
    [SerializeField] List<Transform> socketsSpawn;
    [SerializeField] GameObject prefabScrap;
    [SerializeField] Coroutine co_StartSpawn;
    [SerializeField] float scrapMax;
    [SerializeField] float energyBySecond;
    bool canSpawn;
    List<CS_Scrap> scrapsSpawned;

    private void Start()
    {
        scrapsSpawned = new List<CS_Scrap>();
    }

    public new void StartWork()
    {
        if (battery != null && battery.CurrentEnergy > 0)
        {
            base.StartWork();
            animator.SetBool("IsWork", true);
            co_StartSpawn = StartCoroutine(StartSpawn());
        }
    }

    public new void StopWork()
    {
        base.StopWork();
        animator.SetBool("IsWork", false);
        if (co_StartSpawn != null)
        {
            StopCoroutine(co_StartSpawn);
        }
    }

    public void Drop()
    {
        foreach (var item in scrapsSpawned)
        {
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Rigidbody>().isKinematic = false;
            item.transform.parent = null;
        }
    }

    public void CanSpawn()
    {
        canSpawn = true;
    }

    public void CannotSpawn()
    {
        canSpawn = false;
    }

    public IEnumerator StartSpawn()
    {
        while (true)
        {
            if (canSpawn)
            {
                if (battery == null || battery.CurrentEnergy < energyBySecond)
                {
                    StopWork();
                    DestroyAllScrap();
                    break;
                }
                battery.UseEnergy(energyBySecond);
                if (scrapsSpawned.Count < scrapMax)
                {
                    if (Random.Range(0, 100) < chanceSpawn)
                    {
                        GameObject temp = Instantiate(prefabScrap);
                        temp.transform.position = NextPositionSpawn();
                        temp.GetComponent<Rigidbody>().useGravity = false;
                        temp.GetComponent<Rigidbody>().isKinematic = true;
                        temp.transform.parent = socketsSpawn[0].transform;
                        temp.transform.localRotation = Quaternion.Euler(0, 0, 90);
                        scrapsSpawned.Add(temp.GetComponent<CS_Scrap>());
                    }
                }
            }
            yield return new WaitForSecondsRealtime(1);
        }
    }

    private Vector3 NextPositionSpawn()
    {
        if (scrapsSpawned.Count < socketsSpawn.Count)
        {
            return socketsSpawn[scrapsSpawned.Count].position;
        }
        else
        {
            Vector3 tempPos = socketsSpawn[(scrapsSpawned.Count) % socketsSpawn.Count].position;
            Vector3 decal = (Vector3.down * (scrapsSpawned.Count / socketsSpawn.Count));//socketsSpawn.Count % (scrapsSpawned.Count + 1)));
            Debug.Log(decal);// = Vector3.zero;
            tempPos = tempPos + decal;
            return tempPos;
        }
    }

    private void DestroyAllScrap()
    {
        foreach (var item in scrapsSpawned.ToList())
        {
            Destroy(item.gameObject);
        }
        scrapsSpawned.Clear();
    }
}

