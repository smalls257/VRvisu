using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonSetAxis : MonoBehaviour
{

    public string axis = "";
	// Use this for initialization
	void Start () {
        var btn = transform.GetComponent<Button>();
        btn.onClick.AddListener(setAxis);
    }
	
	// Update is called once per frame
    private void setAxis()
    {
        var scatterPlotGraph = transform.parent.parent.parent.parent.gameObject.GetComponent<scatterPlot>();


        //this will just cycle through and fill in the axis by order: x, y, z 
        //TODO: add queue like implementation
        if (scatterPlotGraph.xAxis == "")
        {
            scatterPlotGraph.xAxis = axis;
        }
        else if (scatterPlotGraph.yAxis == "")
        {
            scatterPlotGraph.yAxis = axis;
        }
        else if (scatterPlotGraph.zAxis == "")
        {
            scatterPlotGraph.zAxis = axis;
            transform.parent.parent.gameObject.SetActiveRecursively(false);
            scatterPlotGraph.createScaleLines();
        }

    }
}
