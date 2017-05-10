using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class axisSelection : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
        Debug.Log("axisSelectionCalled");
	    scatterPlot scatterPlotGraph = transform.parent.parent.parent.gameObject.GetComponent<scatterPlot>();

	    int counter = 0;
        foreach (DataColumn f in scatterPlotGraph.data.Columns)
        {
            Debug.Log(f.ColumnName);
            var tempObj = Instantiate(Resources.Load("Button") as GameObject, new Vector3(0, 0, 0), Quaternion.identity,
                this.transform);

            if (counter >= 16)
            {
                tempObj.transform.localPosition = new Vector3(180, 200, 0);
                tempObj.transform.localPosition -= new Vector3(0, counter % 16 * 50 , 0);
            }

            else if (counter >= 8)
            {
                tempObj.transform.localPosition = new Vector3(0, 200, 0);
                tempObj.transform.localPosition -= new Vector3(0, counter % 8 * 50, 0);
            }

            else if (counter >=0)
            {
                //  tempObj.transform.parent = this.transform;
                tempObj.transform.localPosition = new Vector3(-120, 200, 0);
                tempObj.transform.localPosition -= new Vector3(0, counter * 50, 0);
            }

            
            tempObj.transform.rotation = new Quaternion(0, 0, 0, 0);
            tempObj.AddComponent<buttonSetAxis>();
            tempObj.GetComponent<buttonSetAxis>().axis = f.ColumnName;
            var text = tempObj.GetComponentInChildren<Text>();

            text.text = f.ColumnName;
            counter++;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
