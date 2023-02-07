using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using System.Collections.Specialized;
using System;
using UnityEngine.Profiling;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Experimental.GraphView.Port;

public class SC_WeatherChange : MonoBehaviour
{

    [SerializeField]
    public TimeControler hoursReport;

    [SerializeField]
    public CS_WeatherManager weatherReport;

    public Light nightLight;
    public Light dayLight;

    public Volume VolumeProfile;
    private VolumeProfile cloudProfile;

    public Color ColorcloudA;
    public Color ColorcloudB;


    private bool Normal;
    private bool Rain;
    private bool Storm;
    private bool Day;
    private bool Night;


    void Start()
    {
        cloudProfile = VolumeProfile.sharedProfile;
    }


    void Update()
    {


        if ( hoursReport.currentTime.Minute > hoursReport.sunriseTime.Minutes && hoursReport.currentTime.Minute < hoursReport.sunsetTime.Minutes)
        {
            Day = true;
        }

       else
        {
            Night = true;
        }

        if (weatherReport.WeatherState == Weather.Rainy)
        {
            Rain = true;

        }

        if (weatherReport.WeatherState == Weather.Stormy)
        {
            Storm = true;

        }

        if (weatherReport.WeatherState == Weather.Normal)
        {
            Storm = false;
            Rain = false;

        }

        if (Day == true && Rain == false && Storm == false)
        {
            SunnyDay();

        }

        if (Night == true && Rain == false && Storm == false)
        {
            StarryNight();
        }

        if (Rain == true && Day == true && Storm == false)
        {
            RainyDay();

        }

        if (Rain == true && Night == true && Storm == false)
        {
            RainyNight();

        }

        if (Storm == true && Day == true && Rain == false)
        {
            StormDay();

        }

        if (Storm == true && Night == true && Rain == false)
        {
            StormNight();

        }

    }

    private void SunnyDay()
    {
        if (!cloudProfile.TryGet<CloudLayer>(out var cloud))
        {
            cloud = cloudProfile.Add<CloudLayer>(false);
        }

        cloud.opacity.value = 0.6f;
        cloud.layerA.opacityR.value = 0.4f;
        cloud.layerA.opacityG.value = 0f;
        cloud.layerA.opacityB.value = 0F;
        cloud.layerA.opacityA.value = 0f;

        cloud.layerB.opacityR.value = 0f;
        cloud.layerB.opacityG.value = 0f;
        cloud.layerB.opacityB.value = 0f;
        cloud.layerB.opacityA.value = 0f;

        cloud.layerA.tint.value = ColorcloudA;
        cloud.layerB.tint.value = ColorcloudA;
        hoursReport.maxsunLightIntensity = 10f;
    }

    private void StarryNight()
    {
        if (!cloudProfile.TryGet<CloudLayer>(out var cloud))
        {
            cloud = cloudProfile.Add<CloudLayer>(false);
        }

        cloud.opacity.value = 0.4f;
        cloud.layerA.opacityR.value = 0.04f;
        cloud.layerA.opacityG.value = 0f;
        cloud.layerA.opacityB.value = 0f;
        cloud.layerA.opacityA.value = 0f;

        cloud.layerB.opacityR.value = 0f;
        cloud.layerB.opacityG.value = 0f;
        cloud.layerB.opacityB.value = 0f;
        cloud.layerB.opacityA.value = 0f;

        cloud.layerA.tint.value = ColorcloudA;
        cloud.layerB.tint.value = ColorcloudA;
        hoursReport.maxMoonLightIntensity = 4f;
    }



    private void RainyDay()
    {
        
        if (!cloudProfile.TryGet<CloudLayer>(out var cloud))
        {
            cloud = cloudProfile.Add<CloudLayer>(false);
        }

        cloud.opacity.value = 1f;
        cloud.layerA.opacityR.value = 1f;
        cloud.layerA.opacityG.value = 1f;
        cloud.layerA.opacityB.value = 1f;
        cloud.layerA.opacityA.value = 1f;

        cloud.layerB.opacityR.value = 1f;
        cloud.layerB.opacityG.value = 0.6f;
        cloud.layerB.opacityB.value = 0.164f;
        cloud.layerB.opacityA.value = 0f;
        cloud.layerA.tint.value = ColorcloudA;
        cloud.layerB.tint.value = ColorcloudA;
        hoursReport.maxsunLightIntensity = 10f;
    }


    private void RainyNight()
    {
        if (!cloudProfile.TryGet<CloudLayer>(out var cloud))
        {
            cloud = cloudProfile.Add<CloudLayer>(false);
        }

        cloud.opacity.value = 0.61f;
        cloud.layerA.opacityR.value = 1f;
        cloud.layerA.opacityG.value = 1f;
        cloud.layerA.opacityB.value = 1f;
        cloud.layerA.opacityA.value = 1f;

        cloud.layerB.opacityR.value = 0f;
        cloud.layerB.opacityG.value = 0f;
        cloud.layerB.opacityB.value = 0f;
        cloud.layerB.opacityA.value = 1f;

        cloud.layerA.tint.value = ColorcloudA;
        cloud.layerB.tint.value = ColorcloudB;
        hoursReport.maxMoonLightIntensity = 4f;

    }


    private void StormDay()
    {

        if (!cloudProfile.TryGet<CloudLayer>(out var cloud))
        {
            cloud = cloudProfile.Add<CloudLayer>(false);
        }

        cloud.opacity.value = 1f;
        cloud.layerA.opacityR.value = 1f;
        cloud.layerA.opacityG.value = 1f;
        cloud.layerA.opacityB.value = 1f;
        cloud.layerA.opacityA.value = 1f;

        cloud.layerB.opacityR.value = 0f;
        cloud.layerB.opacityG.value = 0f;
        cloud.layerB.opacityB.value = 0f;
        cloud.layerB.opacityA.value = 1f;

        cloud.layerA.tint.value = ColorcloudB;
        cloud.layerB.tint.value = ColorcloudB;
        hoursReport.maxsunLightIntensity = Mathf.Lerp(10f, 5f, 0.1f);
    }

    private void StormNight()
    {
        if (!cloudProfile.TryGet<CloudLayer>(out var cloud))
        {
            cloud = cloudProfile.Add<CloudLayer>(false);
        }

        cloud.opacity.value = 1f;
        cloud.layerA.opacityR.value = 1f;
        cloud.layerA.opacityG.value = 1f;
        cloud.layerA.opacityB.value = 1f;
        cloud.layerA.opacityA.value = 1f;

        cloud.layerB.opacityR.value = 1f;
        cloud.layerB.opacityG.value = 1f;
        cloud.layerB.opacityB.value = 1f;
        cloud.layerB.opacityA.value = 1f;

        cloud.layerA.tint.value = ColorcloudB;
        cloud.layerB.tint.value = ColorcloudB;

        hoursReport.maxMoonLightIntensity = Mathf.Lerp(4f, 0.5f, 0.1f);
    }
}
