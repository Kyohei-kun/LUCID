using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_WorldIslandParameter: MonoBehaviour
{
    [SerializeField] AnimationCurve nbCristalByHeight;

    public float GetNbCristals(float heightIsland)
    {
        float result = heightIsland.Remap(-1000, 1000, 0, 1);
        result = nbCristalByHeight.Evaluate(result);
        return result;
    }
}
