using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Assets.Scripts;
using Assets.Scripts.Visualizations;
using UnityEngine;

public class scatterPlot : Visualization, iGraphable
{
    private GameObject fileProc;
    private GameObject basicGraph;
    private GameObject scaleLines;
    private GameObject lineLabel;
    private GameObject graph;

    private Vector3 graphSize;
    private Vector3 graphPosition;

    private fileProcessor srcFileProcessor;
    private DataTable data;

  
    public void createShell()
    {
        basicGraph = Resources.Load("basicGraph") as GameObject;
        scaleLines = Resources.Load("scaleLine") as GameObject;
        lineLabel = Resources.Load("lineLabel") as GameObject;
        

        graph = Instantiate(basicGraph, new Vector3(0, 1, 0), Quaternion.identity);
        Debug.Log("!!!!!graphGuiRan!");
        //Set size and position of the graph plane
        graphSize = graph.GetComponent<Collider>().bounds.size;
        graphPosition = graph.transform.position;


        //Create an empty parent object, bars will be created with their own size
        var emptyObject = new GameObject();
        emptyObject.name = "Axis";
        emptyObject.transform.parent = graph.transform;

        //Create xyz Axis, scale them to graph plane to allow proper translation and scaling.
        GameObject zAxis = Instantiate(scaleLines, new Vector3(graphPosition.x - graphSize.x / 2, graphPosition.y, graphPosition.z), Quaternion.identity, emptyObject.transform) as GameObject;
        zAxis.transform.localScale = new Vector3(zAxis.transform.localScale.x, zAxis.transform.localScale.y, graphSize.x);
        zAxis.name = "zAxis";


        int temp = (int)graphPosition.y + (int)scaleLines.transform.GetComponent<Collider>().bounds.size.y;
        GameObject yAxis = Instantiate(scaleLines, new Vector3(graphPosition.x - graphSize.x / 2, graphPosition.y + scaleLines.transform.GetComponent<Collider>().bounds.size.z, graphPosition.z - graphSize.z / 2), Quaternion.identity, emptyObject.transform) as GameObject;
        yAxis.transform.localPosition += new Vector3(0, yAxis.transform.GetComponent<Collider>().bounds.size.z/4, 0);
        yAxis.transform.Rotate(90, 0, 0);
        yAxis.transform.localScale = new Vector3(yAxis.transform.localScale.x, yAxis.transform.localScale.y, graphSize.x);
        yAxis.name = "yAxis";


        GameObject xAxis = Instantiate(scaleLines, new Vector3(graphPosition.x, graphPosition.y, graphPosition.z - graphSize.z / 2), Quaternion.identity, emptyObject.transform) as GameObject;
        xAxis.transform.Rotate(0, 90, 0);
        xAxis.name = "xAxis";
        xAxis.transform.localScale = new Vector3(xAxis.transform.localScale.x, xAxis.transform.localScale.y, graphSize.x);

        plotData();

        //createScaleLines();
    }


    //TODO:REMOVE HARDCODED FILEPATH, ADD CHECKS FOR CORRECT DATA TYPES
    public void plotData()
    {
        fileProc = Resources.Load("FileProcessor") as GameObject;
        srcFileProcessor = fileProc.GetComponent<fileProcessor>();

        string[] cols = new string[3];
        cols[0] = "donor_id";
        cols[1] = "age_in_years";
        cols[2] = "expression_energy_le";

        //Create dataTable and get the dataTypes
        data = srcFileProcessor.createDataTableFromFile(cols);
        Dictionary<string, string> dataTypesDictionary = data.determineDataTypes();

        createScaleLines(5, cols);
    }

    public void reloadVisualization()
    {
        throw new NotImplementedException();
    }

    public void resizeVisualization()
    {
        throw new NotImplementedException();
    }

    public void saveVisualization()
    {
        throw new NotImplementedException();
    }

    public void selectVisualization()
    {
        throw new NotImplementedException();
    }

    // Use this for initialization


    private void adjustTitles(params string[] colNameStrings)
    {
                var Xtext = graph.transform.Find("X-Axis");
                Xtext.GetComponent<TextMesh>().text = colNameStrings[0];
                var Ytext = graph.transform.Find("Y-Axis");
                Ytext.GetComponent<TextMesh>().text = colNameStrings[1];
        
                var Ztext = graph.transform.Find("Z-Axis");
                Ztext.GetComponent<TextMesh>().text = colNameStrings[2];
        
                var title = graph.transform.Find("Title");
                title.GetComponent<TextMesh>().text = "VRvisu - IvyGap Study";
    }
    private void createScaleLines(int ticks, params string[] colNameStrings)
    {

     
        adjustTitles(colNameStrings);

        //set needed variables
        double range;
        double niceMin;
        double niceMax;
        double tickStart;
        double tickSpacing;
        double scaledTickSpacing;
        double tickAmount=0;

        //calc xMin & save for future use
        float minValue = float.MinValue;
        float maxValue = float.MaxValue;
        foreach (DataRow dr in data.Rows)
        {
            float tempFloat;
            float.TryParse(dr[colNameStrings[0]].ToString(), out tempFloat);
          //  float col0 = dr.Field<float>(colNameStrings[0]);
            maxValue = Math.Min(maxValue, tempFloat);
            minValue = Math.Max(minValue, tempFloat);
        }
        xMin = minValue;


        //Create X Scale Lines

        Utilities.createGraphScaling(maxValue, minValue, ticks, out range, out tickSpacing, out niceMin, out niceMax);
        tickStart = niceMin;
        while (tickStart + tickSpacing <= niceMax)
        {
            tickAmount++;
            tickStart += tickSpacing;
        }

     
        tickStart = 0;
        scaledTickSpacing = graphSize.z / tickAmount;

        //used for plot points
        xScaledTickSpacing = (float) scaledTickSpacing;
        xTickSpacing = (float)tickSpacing;

        var xEmptyObject = new GameObject();
        xEmptyObject.name = "xLines";
        xEmptyObject.transform.parent = graph.gameObject.transform;


        //Creates Line & Labels for Scale
        for (int i = 0; i <= tickAmount; i++)
        {
            GameObject xline =
                Instantiate(scaleLines,
                    new Vector3(graphPosition.x, graphPosition.y, graphPosition.z - graphSize.z / 2 + (float)tickStart),
                    Quaternion.identity, xEmptyObject.transform) as GameObject;
            xline.transform.Rotate(0, 90, 0);
            xline.name = "xline" + i.ToString();
            xline.transform.localScale = new Vector3(xline.transform.localScale.x, xline.transform.localScale.y,
                graphSize.x);
            //  Debug.Log("xline created at: " + xline.transform.position.z);

            GameObject xlineLabel =
                Instantiate(lineLabel,
                    new Vector3(0,0,0), Quaternion.identity,
                    xEmptyObject.transform) as GameObject;
            xlineLabel.transform.localPosition +=
                new Vector3(graph.transform.GetComponent<Collider>().bounds.size.z / 1.5f + graphPosition.x,
                    graphPosition.y, graphPosition.z - graphSize.z / 2 + (float) tickStart);
            xlineLabel.name = "xlineLabel " + i.ToString();
            xlineLabel.transform.Rotate(0, 180, 0);
            
            TextMesh tm = xlineLabel.GetComponent<TextMesh>();
            tm.text = niceMin.ToString();

            tickStart += scaledTickSpacing;
            niceMin += tickSpacing;

        }



   

        //Create Y Scale Lines
        maxValue = float.MinValue;
        minValue = float.MaxValue;

        foreach (DataRow dr in data.Rows)
        {
            float tempFloat;
            float.TryParse(dr[colNameStrings[1]].ToString(), out tempFloat);
            minValue = Math.Min(minValue, tempFloat);
            maxValue = Math.Max(maxValue, tempFloat);
        }
        yMin = minValue;
        Debug.Log("Min: "+minValue+" Max: "+maxValue);

        Utilities.createGraphScaling(minValue, maxValue, ticks, out range, out tickSpacing, out niceMin, out niceMax);
          Debug.Log("range: " + range + "\n min: " + niceMin);
          Debug.Log("max: " + niceMax + "\n tickspace: " + tickSpacing);

        //store amount ticks we need to create, scale tickSpacing down to the graphsize, reset tickStart
        tickStart = niceMin;
        tickAmount = 0;
        while (tickStart + tickSpacing <= niceMax)
        {
            tickAmount++;
            tickStart += tickSpacing;
        }

        tickStart = 0;
        scaledTickSpacing = graphSize.z / tickAmount;


        //used for plot points
        yScaledTickSpacing = (float)scaledTickSpacing;
        yTickSpacing = (float)tickSpacing;

        var yEmptyObject = new GameObject();
        yEmptyObject.name = "yLines";
        yEmptyObject.transform.parent = graph.transform;

        //Creates Line & Labels for Scale
        for (int i = 0; i <= tickAmount; i++)
        {
            GameObject yline =
                Instantiate(scaleLines,
                    new Vector3(graphPosition.x, graphPosition.y + (float)tickStart, graphPosition.z - graphSize.z / 2),
                    Quaternion.identity, yEmptyObject.transform) as GameObject;
            yline.transform.Rotate(0, 90, 0);
            yline.name = "yline";
            yline.transform.localScale = new Vector3(yline.transform.localScale.x, yline.transform.localScale.y,
                graphSize.x);
            //  Debug.Log("yline created at: " + yline.transform.position.z);

            GameObject yLineLabel =
                Instantiate(lineLabel,
                    new Vector3(graph.transform.GetComponent<Collider>().bounds.size.z / 2.5f - graph.transform.position.x,
                        graphPosition.y + (float)tickStart, graphPosition.z - graphSize.z / 2), Quaternion.identity,
                    yEmptyObject.transform) as GameObject;
            yLineLabel.name = "ylineLabel " + i.ToString();
            yLineLabel.transform.Rotate(0, 180, 0);
            TextMesh tm = yLineLabel.GetComponent<TextMesh>();
            tm.text = niceMin.ToString();

            tickStart += scaledTickSpacing;
            niceMin += tickSpacing;

        }


        //Create Z Scale Lines
        maxValue = float.MinValue;
        minValue = float.MaxValue;

        foreach (DataRow dr in data.Rows)
        {
            float tempFloat;
            float.TryParse(dr[colNameStrings[2]].ToString(), out tempFloat);
            //  float col0 = dr.Field<float>(colNameStrings[0]);
            minValue = Math.Min(minValue, tempFloat);
            maxValue = Math.Max(maxValue, tempFloat);
        }

        zMin = minValue;

        Utilities.createGraphScaling(minValue, maxValue, ticks, out range, out tickSpacing, out niceMin, out niceMax);

        //store amount ticks we need to create, scale tickSpacing down to the graphsize, reset tickStart
        tickStart = niceMin;
        tickAmount = 0;
        while (tickStart + tickSpacing <= niceMax)
        {
            tickAmount++;
            tickStart += tickSpacing;
        }

        tickStart = 0;
        scaledTickSpacing = graphSize.z / tickAmount;


        //used for plot points
        zScaledTickSpacing = (float)scaledTickSpacing;
        zTickSpacing = (float)tickSpacing;

        var zEmptyObject = new GameObject();
        zEmptyObject.name = "zLines";
        zEmptyObject.transform.parent = graph.transform;


        //Creates Line & Labels for Scale
        for (int i = 0; i <= tickAmount; i++)
        {
            GameObject zLine =
                Instantiate(scaleLines,
                    new Vector3(graphPosition.x - (graphSize.z / 2) + (float)tickStart, graphPosition.y,
                        graphPosition.z), Quaternion.identity, zEmptyObject.transform) as GameObject;
            zLine.transform.Rotate(0, 0, 90);
            zLine.name = "zline";
            zLine.transform.localScale = new Vector3(zLine.transform.localScale.x, zLine.transform.localScale.y,
                graphSize.x);


            //Debug.Log("xline created at: " + zLine.transform.position.z);

            GameObject zLineLabel =
                Instantiate(lineLabel,
                    new Vector3(0,0,0), Quaternion.identity,
                    zEmptyObject.transform) as GameObject;

            zLineLabel.transform.localPosition +=
               new Vector3(graphPosition.z - graphSize.z / 2 + (float)tickStart,
                  graph.transform.position.y, graph.transform.GetComponent<Collider>().bounds.size.z / 1.5f + graph.transform.position.z );


            zLineLabel.name = "zlineLabel " + i.ToString();
            zLineLabel.transform.Rotate(0, 180, 0);
            TextMesh tm = zLineLabel.GetComponent<TextMesh>();
            tm.text = niceMin.ToString();

            tickStart += scaledTickSpacing;
            niceMin += tickSpacing;

        }

        genPlotPoints(data, colNameStrings);
    }

    private Vector3 pointLoc;
    private float xMin;
    private float yMin;
    private float zMin;

    private float xTickSpacing;
    private float yTickSpacing;
    private float zTickSpacing;

    private float xScaledTickSpacing;
    private float yScaledTickSpacing;
    private float zScaledTickSpacing;


    private void genPlotPoints(DataTable data, params string[] colNameStrings)
    {
        pointLoc = graph.transform.position;
        Debug.Log(pointLoc);


        //Index of the list will be the row number of the data table - 1
        //Index is useful for finding unique values that correspond to datatable, think dataWindows
        List<Vector3> positions = new List<Vector3>();


        Debug.Log("GenPlotPoints");


        //get the values and calculate vector positions for the points
        //make sure to start from the 1st row of data!
        int rowIndex = 0;
        foreach (DataRow item in data.Rows)
        {
            // skip header row
           // if (rowIndex++ == 0) continue;

            //get the values from the row, parse to a float
            //subtract the min so we can calculate displacement from minimum
            string tempStr = item[colNameStrings[0]].ToString();
            //Debug.Log("tempSTR: " + tempStr);
            float xTemp = float.Parse(tempStr) - xMin;
            tempStr = item[colNameStrings[1]].ToString();
            float yTemp = float.Parse(tempStr) - yMin;
            tempStr = item[colNameStrings[2]].ToString();
            float zTemp = float.Parse(tempStr) - zMin;

            //find the ratio of the displacement to the graph itself. This determines location
            xTemp = xTemp / xTickSpacing;
            yTemp = yTemp / yTickSpacing;
            zTemp = zTemp / zTickSpacing;
         
            
            xTemp *= xScaledTickSpacing;
            yTemp *= yScaledTickSpacing;
            zTemp *= zScaledTickSpacing;


            float temp = 0 - graph.transform.localScale.x;
            temp = temp / 2;

            xTemp += temp + graph.transform.localScale.z;
            //fixes to adjust for the height of the graph
            yTemp = yTemp + graph.transform.position.y;
            zTemp += temp;


            //finally make our position vector
            Vector3 tempLocVector = new Vector3(xTemp, yTemp, zTemp);

            //add to list
            positions.Add(tempLocVector);
        }

        Debug.Log("Positions length: "+positions.Count);


        //Make a parent to group the plotpoints
        var emptyObject = new GameObject();
        emptyObject.transform.parent = graph.transform;

        //TODO: REMOVE THIS TEST CODE
        var csvData = data.ToCsv();
        File.WriteAllText("test.csv", csvData);


        //make a counter for the inner parts of loop, also helps match models/meshes
        int i = 0;
        foreach (Vector3 item in positions)
        {

            GameObject newthing =
                Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere),
                    new Vector3(item.z + pointLoc.x, item.y, item.x + pointLoc.z), Quaternion.identity,
                    emptyObject.transform) as GameObject;

            newthing.name = "plotPoint " + i.ToString();

            newthing.transform.localScale = new Vector3(.1f, .1f, .1f);

            i++;
        }

    }
}
