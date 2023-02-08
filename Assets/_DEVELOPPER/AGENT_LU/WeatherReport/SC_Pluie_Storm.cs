using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SC_Pluie_Storm : MonoBehaviour
{
    [SerializeField]
    public CS_WeatherManager weatherMode2;

    public ParticleSystem PluieStorm;




    void Update()
    {

        if (weatherMode2.WeatherState == Weather.Stormy)
        {
            PluieStorm.Play();


        }

        if (weatherMode2.WeatherState == Weather.Normal)
        {
            PluieStorm.Pause();

        }
    }
}
