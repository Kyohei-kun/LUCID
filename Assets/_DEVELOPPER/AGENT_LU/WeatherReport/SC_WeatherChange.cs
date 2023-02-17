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
//using static UnityEditor.Experimental.GraphView.GraphView;
//using static UnityEditor.Experimental.GraphView.Port;
using UnityEngine.Rendering.LookDev;
using Unity.VisualScripting;

public class SC_WeatherChange : MonoBehaviour
{

    [SerializeField]
    public TimeControler hoursReport;

    [SerializeField]
    public CS_WeatherManager weatherReport;

    [SerializeField]
    private Material DownClouds;

    //Down Cloud Color
    public Color DownCloud_Day, DownCloud_Night, DownCloud_RainyNight, DownCloud_StormyNight, DownCloud_RainyDay, DownCloud_StormyDay;
    private Color ActualDColor;

    //Time Controler Lights
    public Light nightLight, dayLight;

    //Sky and Fog Volume
    public Volume VolumeProfile;
    private VolumeProfile cloudProfile;

    //Cloud Layer Color
    public Color ColorcloudA, ColorcloudB;

    //Boolean Weather
    private bool Rain, Storm, Day, Night;

    //Cloud Layer A Opacity
    private float cloudOpacityR, cloudOpacityG, cloudOpacityA, cloudOpacityB, cloudOpa;

    //Cloud Layer B Opacity
    private float BcloudOpacityR, BcloudOpacityG, BcloudOpacityB, BcloudOpacityA;

    //Grount Tint Color
    public Color DayNightCloud, RainyDayCloud, RainyNightCloud, StormyNightCloud, StormyDayCloud;


    void Start()
    {
        cloudProfile = VolumeProfile.sharedProfile;
    }


    void Update()
    {
        if (!cloudProfile.TryGet<CloudLayer>(out var cloud))
        {
            cloud = cloudProfile.Add<CloudLayer>(false);
        }

        cloudOpa = cloud.opacity.value;
        cloudOpacityR = cloud.layerA.opacityR.value;
        cloudOpacityG = cloud.layerA.opacityG.value;
        cloudOpacityB = cloud.layerA.opacityB.value;
        cloudOpacityA = cloud.layerA.opacityA.value;

        BcloudOpacityR = cloud.layerB.opacityR.value;
        BcloudOpacityG = cloud.layerB.opacityG.value;
        BcloudOpacityB = cloud.layerB.opacityB.value;
        BcloudOpacityA = cloud.layerB.opacityA.value;

        DownClouds.SetColor("_Color", ActualDColor);


        if ( hoursReport.currentTime.Hour > hoursReport.sunriseTime.Hours && hoursReport.currentTime.Hour < hoursReport.sunsetTime.Hours)
        {
            Day = true;
            Night = false;

            if (!cloudProfile.TryGet<PhysicallyBasedSky>(out var sky))
            {
                sky = cloudProfile.Add<PhysicallyBasedSky>(false);
            }

            sky.groundColorTexture = new CubemapParameter(sky.groundColorTexture.value, false);
            sky.groundEmissionTexture = new CubemapParameter(sky.groundColorTexture.value, false);
            sky.spaceEmissionTexture = new CubemapParameter(sky.groundColorTexture.value, false);
            sky.spaceEmissionMultiplier.value = 0f;

        }

        else
        {
            Night = true;
            Day = false;

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

        if (Day == true && Rain == false && Storm == false && Night == false)
        {
            SunnyDay();

        }

        if (Night == true && Rain == false && Storm == false && Day == false)
        {
            StarryNight();
        }

        if (Rain == true && Day == true && Storm == false && Night == false)
        {
            RainyDay();

        }

        if (Rain == true && Night == true && Storm == false && Day == false )
        {
            RainyNight();

        }

        if (Storm == true && Day == true && Rain == false && Night == false)
        {
            StormDay();

        }

        if (Storm == true && Night == true && Rain == false && Day == false)
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

        if (!cloudProfile.TryGet<PhysicallyBasedSky>(out var sky))
        {
            sky = cloudProfile.Add<PhysicallyBasedSky>(false);
        }

        cloud.opacity.value = Mathf.Lerp(cloudOpa, 0.6f, 0.1f);
        cloud.layerA.opacityR.value = Mathf.Lerp(cloudOpacityR, 0.4f, 0.1f);
        cloud.layerA.opacityG.value = Mathf.Lerp(cloudOpacityG,0f, 0.1f);
        cloud.layerA.opacityB.value = Mathf.Lerp(cloudOpacityB,0f, 0.1f);
        cloud.layerA.opacityA.value = Mathf.Lerp(cloudOpacityA,0f, 0.1f);

        cloud.layerB.opacityR.value = Mathf.Lerp(BcloudOpacityR,0f ,0.1f);
        cloud.layerB.opacityG.value = Mathf.Lerp(BcloudOpacityG,0f,0.1f);
        cloud.layerB.opacityB.value = Mathf.Lerp(BcloudOpacityB,0f,0.1f);
        cloud.layerB.opacityA.value = Mathf.Lerp(BcloudOpacityA,0f,0.1f);

        cloud.layerA.tint.value = ColorcloudA;
        cloud.layerB.tint.value = ColorcloudA;
        hoursReport.maxsunLightIntensity = 10f;

        sky.groundTint.value = DayNightCloud;

        ActualDColor = DownCloud_Day;

        //Debug.Log("Day");

    }

    private void StarryNight()
    {
        if (!cloudProfile.TryGet<CloudLayer>(out var cloud))
        {
            cloud = cloudProfile.Add<CloudLayer>(false);
        }

        if (!cloudProfile.TryGet<PhysicallyBasedSky>(out var sky))
        {
            sky = cloudProfile.Add<PhysicallyBasedSky>(false);
        }

        cloud.opacity.value = Mathf.Lerp(cloudOpa, 0f,0.1f) ;
        cloud.layerA.opacityR.value = Mathf.Lerp(cloudOpacityR, 0f,0.1f);
        cloud.layerA.opacityG.value = Mathf.Lerp(cloudOpacityG, 0f,0.1f);
        cloud.layerA.opacityB.value = Mathf.Lerp(cloudOpacityB, 0f,0.1f);
        cloud.layerA.opacityA.value = Mathf.Lerp(cloudOpacityA, 0f,0.1f);

        cloud.layerB.opacityR.value = Mathf.Lerp(BcloudOpacityR, 0f,0.1f);
        cloud.layerB.opacityG.value = Mathf.Lerp(BcloudOpacityG, 0f,0.1f);
        cloud.layerB.opacityB.value = Mathf.Lerp(BcloudOpacityB, 0f,0.1f);
        cloud.layerB.opacityA.value = Mathf.Lerp(BcloudOpacityA, 0f,0.1f);

        cloud.layerA.tint.value = ColorcloudA;
        cloud.layerB.tint.value = ColorcloudA;
        hoursReport.maxMoonLightIntensity = 4f;

        sky.groundEmissionTexture = new CubemapParameter(sky.groundColorTexture.value, true);
        sky.groundColorTexture = new CubemapParameter(sky.groundColorTexture.value, true);
        sky.spaceEmissionTexture = new CubemapParameter(sky.groundColorTexture.value, true);
        sky.spaceEmissionMultiplier.value = 10f;
        sky.groundTint.value = DayNightCloud;

        ActualDColor = DownCloud_Night;

        //Debug.Log("Night");
    }



    private void RainyDay()
    {
        
        if (!cloudProfile.TryGet<CloudLayer>(out var cloud))
        {
            cloud = cloudProfile.Add<CloudLayer>(false);
        }

        if (!cloudProfile.TryGet<PhysicallyBasedSky>(out var sky))
        {
            sky = cloudProfile.Add<PhysicallyBasedSky>(false);
        }

        cloud.opacity.value = Mathf.Lerp(cloudOpa, 1f,0.1f);
        cloud.layerA.opacityR.value = Mathf.Lerp(cloudOpacityR, 1f,0.1f);
        cloud.layerA.opacityG.value = Mathf.Lerp(cloudOpacityG, 1f,0.1f);
        cloud.layerA.opacityB.value = Mathf.Lerp(cloudOpacityB, 1f,0.1f);
        cloud.layerA.opacityA.value = Mathf.Lerp(cloudOpacityA, 1f,0.1f);

        cloud.layerB.opacityR.value = Mathf.Lerp(BcloudOpacityR, 1f,0.1f);
        cloud.layerB.opacityG.value = Mathf.Lerp(BcloudOpacityG, 0.6f,0.1f);
        cloud.layerB.opacityB.value = Mathf.Lerp(BcloudOpacityB, 0.164f,0.1f);
        cloud.layerB.opacityA.value = Mathf.Lerp(BcloudOpacityA, 0f,0.1f);
        cloud.layerA.tint.value = ColorcloudA;
        cloud.layerB.tint.value = ColorcloudA;
        hoursReport.maxsunLightIntensity = 8f;

        sky.groundTint.value = RainyDayCloud;

        ActualDColor = DownCloud_RainyDay;
        //Debug.Log("RainyDay");


    }


    private void RainyNight()
    {
        if (!cloudProfile.TryGet<CloudLayer>(out var cloud))
        {
            cloud = cloudProfile.Add<CloudLayer>(false);
        }

        if (!cloudProfile.TryGet<PhysicallyBasedSky>(out var sky))
        {
            sky = cloudProfile.Add<PhysicallyBasedSky>(false);
        }

        cloud.opacity.value = Mathf.Lerp(cloudOpa, 0.61f,0.1f);
        cloud.layerA.opacityR.value = Mathf.Lerp(cloudOpacityR, 1f,0.1f);
        cloud.layerA.opacityG.value = Mathf.Lerp(cloudOpacityG, 1f,0.1f);
        cloud.layerA.opacityB.value = Mathf.Lerp(cloudOpacityB, 1f,0.1f);
        cloud.layerA.opacityA.value = Mathf.Lerp(cloudOpacityA, 1f, 0.1f);

        cloud.layerB.opacityR.value = Mathf.Lerp(BcloudOpacityR, 0f,0.1f);
        cloud.layerB.opacityG.value = Mathf.Lerp(BcloudOpacityG, 0f,0.1f);
        cloud.layerB.opacityB.value = Mathf.Lerp(BcloudOpacityB, 0f,0.1f);
        cloud.layerB.opacityA.value = Mathf.Lerp(BcloudOpacityA, 1f,0.1f);

        cloud.layerA.tint.value = ColorcloudA;
        cloud.layerB.tint.value = ColorcloudB;
        hoursReport.maxMoonLightIntensity = 4f;

        sky.groundTint.value = RainyNightCloud;

        ActualDColor = DownCloud_RainyNight;

        //Debug.Log("RainyNight");
    }


    private void StormDay()
    {

        if (!cloudProfile.TryGet<CloudLayer>(out var cloud))
        {
            cloud = cloudProfile.Add<CloudLayer>(false);
        }

        if (!cloudProfile.TryGet<PhysicallyBasedSky>(out var sky))
        {
            sky = cloudProfile.Add<PhysicallyBasedSky>(false);
        }

        cloud.opacity.value = Mathf.Lerp(cloudOpa, 1f, 0.1f);
        cloud.layerA.opacityR.value = Mathf.Lerp(cloudOpacityR, 1f,0.1f);
        cloud.layerA.opacityG.value = Mathf.Lerp(cloudOpacityG,1f,0.1f);
        cloud.layerA.opacityB.value = Mathf.Lerp(cloudOpacityB, 1f,0.1f);
        cloud.layerA.opacityA.value = Mathf.Lerp(cloudOpacityA, 1f,0.1f);

        cloud.layerB.opacityR.value = Mathf.Lerp(BcloudOpacityR, 0f,0.1f);
        cloud.layerB.opacityG.value = Mathf.Lerp(BcloudOpacityG, 0f,0.1f);
        cloud.layerB.opacityB.value = Mathf.Lerp(BcloudOpacityB, 0f,0.1f);
        cloud.layerB.opacityA.value = Mathf.Lerp(BcloudOpacityA, 1f,0.1f);

        cloud.layerA.tint.value = ColorcloudB;
        cloud.layerB.tint.value = ColorcloudB;
        hoursReport.maxsunLightIntensity = Mathf.Lerp(10f, 5f, 0.1f);

        sky.groundTint.value = StormyDayCloud;

        ActualDColor = DownCloud_StormyDay;

        //Debug.Log("StormDay");
    }

    private void StormNight()
    {
        if (!cloudProfile.TryGet<CloudLayer>(out var cloud))
        {
            cloud = cloudProfile.Add<CloudLayer>(false);
        }

        if (!cloudProfile.TryGet<PhysicallyBasedSky>(out var sky))
        {
            sky = cloudProfile.Add<PhysicallyBasedSky>(false);
        }

        cloud.opacity.value = Mathf.Lerp(cloudOpa, 1f,0.1f);
        cloud.layerA.opacityR.value = Mathf.Lerp(cloudOpacityR, 1f,0.1f);
        cloud.layerA.opacityG.value = Mathf.Lerp(cloudOpacityG, 1f,0.1f);
        cloud.layerA.opacityB.value = Mathf.Lerp(cloudOpacityB, 1f,0.1f);
        cloud.layerA.opacityA.value = Mathf.Lerp(cloudOpacityA, 1f,0.1f);

        cloud.layerB.opacityR.value = Mathf.Lerp(BcloudOpacityR, 1f,0.1f);
        cloud.layerB.opacityG.value = Mathf.Lerp(BcloudOpacityG, 1f,0.1f);
        cloud.layerB.opacityB.value = Mathf.Lerp(BcloudOpacityB, 1f,0.1f);
        cloud.layerB.opacityA.value = Mathf.Lerp(BcloudOpacityA, 1f,0.1f);

        cloud.layerA.tint.value = ColorcloudB;
        cloud.layerB.tint.value = ColorcloudB;

        hoursReport.maxMoonLightIntensity = Mathf.Lerp(4f, 0.5f, 0.1f);
        sky.groundTint.value = StormyNightCloud;

        ActualDColor = DownCloud_StormyNight;
        //Debug.Log("StormNight");

    }
}
