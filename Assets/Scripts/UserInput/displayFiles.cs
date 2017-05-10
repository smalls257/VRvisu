using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class displayFiles : MonoBehaviour
{

   // public string filePath;

	// Use this for initialization
    void Start()
    {
        //Lists items in dir
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/StreamingAssets");
        FileInfo[] info = dir.GetFiles("*.*");

        int counter = 0;
        foreach (FileInfo f in info)
        {
            var tempObj = Instantiate(Resources.Load("Button") as GameObject, new Vector3(0, 0, 0), Quaternion.identity,
                this.transform);

            //  tempObj.transform.parent = this.transform;
            tempObj.transform.localPosition = new Vector3(-180, 200, 0);
            tempObj.transform.localPosition -= new Vector3(0, counter * 50, 0);
            tempObj.transform.rotation = new Quaternion(0, 0, 0, 0);
            tempObj.AddComponent<buttonSetFilePath>();

            var text = tempObj.GetComponentInChildren<Text>();

            string tempString = f.ToString();
            tempObj.GetComponent<buttonSetFilePath>().filePath = f.ToString();
            //[^\\]+(\.).*
            string pattern = @"[^\\]+(\.).*";
            // string pattern = @"(?=.)[^\\\\]*(.xls)";

            Match match = Regex.Match(tempString, pattern);

            text.text = match.Groups[0].Value;


            //Debug.Log(filePath);
            counter++;
        }

    }
}
