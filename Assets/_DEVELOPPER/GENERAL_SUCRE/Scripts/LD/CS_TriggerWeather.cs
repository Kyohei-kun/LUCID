using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_TriggerWeather : MonoBehaviour
{
    CS_WeatherManager manager;
    [SerializeField] Weather stateTriger;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("WeatherManager").GetComponent<CS_WeatherManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            manager.WeatherState = stateTriger;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            manager.WeatherState = Weather.Normal;
        }
    }
}
