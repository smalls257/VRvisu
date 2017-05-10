using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
    public GameObject exitButton;

    private GameObject layer1;
    private GameObject visualizationsSubMenu;

    // Use this for initialization
    void Start()
    {
        layer1 = transform.Find("MinimalPanel/Layer1").gameObject;
        visualizationsSubMenu = transform.Find("MinimalPanel/VisualizationsSubMenu").gameObject;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void menuToggle()
    {
        if (layer1.activeSelf == true)
        {
            layer1.SetActive(false);
        }


        else
        {
            layer1.SetActive(true);
        }
    }

    public void visualizationsToggle()
    {
        if (visualizationsSubMenu.activeSelf == true)
        {
            visualizationsSubMenu.SetActive(false);
        }


        else
        {
            visualizationsSubMenu.SetActive(true);
        }
    }
    public void exitApp()
    {
        Application.Quit();
    }


}
