using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SC_Pluie : MonoBehaviour
{
    [SerializeField]
    public CS_WeatherManager weatherMode1;

    public ParticleSystem Pluie;


   
    void Update()
    {

        if (weatherMode1.WeatherState == Weather.Rainy)
        {
            Pluie.Play();


        }

        if (weatherMode1.WeatherState == Weather.Normal)
        {
            Pluie.Pause();

        }

        if (weatherMode1.WeatherState == Weather.Stormy)
        {
            Pluie.Play();


        }
    }
}
