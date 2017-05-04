using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualization : MonoBehaviour
{
    
    public bool isSelected = false;
    
    public void destroyVisualization()
    {
        Destroy(this.gameObject);
    }

    //TODO: IMPLEMENT SAVE
    //TODO: IMPLMENT SELECT


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            destroyVisualization();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            scatterPlot scr = new scatterPlot();
            scr.createShell();
        }
    }
}
