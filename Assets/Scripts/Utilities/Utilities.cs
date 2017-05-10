using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;


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

    public static void createDataWindow(DataTable data, GameObject dataPoint, int dataRowNumber)
    {
        //get the names and values from the dataRow
        string windowString = "";
        int i = 0;
        foreach (var item in data.Rows[dataRowNumber].ItemArray)
        {
            Debug.Log(data.Columns[i].ColumnName + ": "+ item);
            windowString += data.Columns[i].ColumnName + ": " + item + "\n";
            i++;
        }

        
        GameObject dataWindow = Instantiate(Resources.Load("DataWindow") as GameObject,new Vector3(0,0,0), Quaternion.identity, dataPoint.transform);

        //set text and tweak the settings to look nice
        dataWindow.GetComponentInChildren<Text>().text = windowString;
        dataWindow.transform.localScale *= 10;
        dataWindow.transform.localPosition = new Vector3(7.73f,0,0);
        dataWindow.transform.rotation = new Quaternion(0,180,0,0);

        //auto-disables, a trigger collision should enable it
        dataWindow.SetActiveRecursively(false);
    }
}
