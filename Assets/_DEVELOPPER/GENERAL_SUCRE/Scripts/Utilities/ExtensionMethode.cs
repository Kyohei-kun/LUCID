using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethode
{
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static float ClampAngle(this float lfAngle, float lfMin, float lfMax)
    {
        //if (lfAngle < -360f) lfAngle += 360f;
        //if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
