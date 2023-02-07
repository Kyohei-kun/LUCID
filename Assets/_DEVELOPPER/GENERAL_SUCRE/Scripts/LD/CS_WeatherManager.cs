using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weather
{
    Normal,
    Stormy,
    Rainy,
}

public class CS_WeatherManager : MonoBehaviour
{
    [SerializeField] Weather weatherState;

    public Weather WeatherState { get => weatherState; set => weatherState = value; }

}
