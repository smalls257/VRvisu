using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationData : MonoBehaviour
{

    public string filePath;
    public string xAxis;
    public string yAxis;
    public string zAxis;

    public void setXAxis(string xAxis)
    {
        Debug.Log("xAsix set");
        this.xAxis = xAxis;
    }

    public void setFilePath(string filePath)
    {
        this.filePath = filePath;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
