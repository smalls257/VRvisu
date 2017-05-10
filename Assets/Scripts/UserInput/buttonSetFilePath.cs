using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonSetFilePath : MonoBehaviour
{

    public string filePath = "";
	// Use this for initialization
	void Start ()
	{
	    var btn = transform.GetComponent<Button>();
        btn.onClick.AddListener(setFilePath);
	}
    public void setFilePath()
    {
        //grabs the parent Graph to set the filePath
        transform.parent.parent.parent.parent.gameObject.GetComponent<scatterPlot>().filePath = filePath;

        transform.parent.parent.parent.parent.gameObject.GetComponent<scatterPlot>().createDataTable();

        //disable and enable appropriate menus
        transform.parent.parent.gameObject.SetActiveRecursively(false);
        transform.parent.parent.parent.gameObject.transform.FindChild("AxisSelection").gameObject.SetActiveRecursively(true);

    }
    // Update is called once per frame
    void Update () {
		
	}
}
