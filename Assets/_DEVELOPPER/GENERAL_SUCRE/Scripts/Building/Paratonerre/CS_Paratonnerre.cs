using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Paratonnerre : CS_ElectricBuilding
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject lightningSprite;
    Coroutine co;
    CS_WeatherManager weatherManager;
    [SerializeField] float currentChance;
    [SerializeField] float startChance;
    [SerializeField] float chanceStep;
    [SerializeField] GameObject fx;
    [SerializeField] Material sourceMaterial;
    [SerializeField] AnimationCurve fxAlphaCurve;

    [Space(10)]
    [SerializeField] Vector2 chargeBatteryByThunderBolt;

    private void Start()
    {
        weatherManager = GameObject.FindGameObjectWithTag("WeatherManager").GetComponent<CS_WeatherManager>();
        currentChance = startChance;
        sourceMaterial.SetFloat("_Alpha", 0);
    }

    public new void StartWork()
    {
        base.StartWork();
        animator.SetBool("IsActive", true);
        co = StartCoroutine(ManualUpdate());
    }

    public new void StopWork()
    {
        base.StopWork();
        animator.SetBool("IsActive", false);
        StopCoroutine(co);
        co = null;
    }

    private IEnumerator ManualUpdate()
    {
        while (true)
        {
            if (weatherManager.WeatherState == Weather.Stormy)
            {
                if (Random.Range(0, 100) < currentChance)
                {
                    lightningSprite.SetActive(true);
                    yield return new WaitForSecondsRealtime(0.05f);
                    lightningSprite.SetActive(false);
                    currentChance = startChance;
                    if(battery!= null)
                    {
                        battery.Charge(Random.Range(chargeBatteryByThunderBolt.x, chargeBatteryByThunderBolt.y));
                    }
                    float startTimer = Time.time;
                    
                    while((Time.time - startTimer) < (fxAlphaCurve.keys[fxAlphaCurve.length - 1].time))
                    {
                        sourceMaterial.SetFloat("_Alpha", fxAlphaCurve.Evaluate(Time.time - startTimer));
                        yield return 0;
                    }
                    sourceMaterial.SetFloat("_Alpha", 0);
                }
                else
                {
                    currentChance += chanceStep;
                }
            }
            yield return new WaitForSecondsRealtime(10);
        }
    }
}
