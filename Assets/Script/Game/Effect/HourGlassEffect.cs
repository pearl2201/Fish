using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HourGlassEffect : MonoBehaviour
{

    public Transform hourGlass;
    public Vector3 begin;
    public Vector3 end;
    public bool fromBeginToEnd;
    public float t;
    public float duration;

    void Start()
    {
        hourGlass.localScale = begin;
        fromBeginToEnd = true;
    }

    void Update()
    {
        t += Time.deltaTime;
        if (fromBeginToEnd)
        {
            hourGlass.localScale = Vector3.Lerp(begin, end, t / duration);
        }
        else
        {
            hourGlass.localScale = Vector3.Lerp(end, begin, t / duration);
        }

        if (t > duration)
        {
            fromBeginToEnd = !fromBeginToEnd;
            t = 0;
        }
    }
}

