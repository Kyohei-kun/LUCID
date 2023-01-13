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

    public static List<TSource> MoveToFirst<TSource>(this List<TSource> source, TSource element)
    {/*from  www  .ja v a2s .co m*/
        if (!source.Contains(element))
            return source;

        source.Remove(element);
        source.Insert(0, element);
        return source;
    }


    //public static List<E_Item> ToEnum<CS_Item>(this ICollection<CS_Item> items)
    //{
    //        List<E_Item> result = new List<E_Item>();

    //        CS_Item temp;

    //        //foreach (CS_Item item in items)
    //        //{
    //        //    result.Add(item)
    //        //}
    //        return null;

    //}
}
