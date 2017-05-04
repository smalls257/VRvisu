using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Utilities : MonoBehaviour {


    public void utilities()
    {
        
    }
    //Scaling Functions
    public static void createGraphScaling(float min, float max, int maxTicks, out double range, out double tickSpacing, out double niceMin, out double niceMax)
    {
        range = niceNum(max - min, false);
        tickSpacing = niceNum(range / (maxTicks - 1), true);
        niceMin = System.Math.Floor(min / tickSpacing) * tickSpacing;
        niceMax = System.Math.Ceiling(max / tickSpacing) * tickSpacing;
    }

    private static double niceNum(double range, bool round)
    {
        double pow = System.Math.Pow(10, Math.Floor(Math.Log10(range)));
        double fraction = range / pow;

        double niceFraction;
        if (round)
        {
            if (fraction < 1.5)
            {
                niceFraction = 1;
            }
            else if (fraction < 3)
            {
                niceFraction = 2;
            }
            else if (fraction < 7)
            {
                niceFraction = 5;
            }
            else
            {
                niceFraction = 10;
            }
        }
        else
        {
            if (fraction <= 1)
            {
                niceFraction = 1;
            }
            else if (fraction <= 2)
            {
                niceFraction = 2;
            }
            else if (fraction <= 5)
            {
                niceFraction = 5;
            }
            else
            {
                niceFraction = 10;
            }
        }

        return niceFraction * pow;
    }
}
