using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            Debug.Log("Called");
            Instantiate(Resources.Load("basicGraph"));
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            
        }
    }
}
