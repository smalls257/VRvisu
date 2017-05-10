using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Assets.Scripts;
using UnityEngine;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using UnityEngine.SocialPlatforms;
using NPOI.XSSF.UserModel;

public class fileProcessor : MonoBehaviour {


	// Use this for initialization
	void Start ()
	{
//        string[] cols = new string[3];
//	    cols[0] = "donor_id";
//	    cols[1] = "age_in_years";
//	    cols[2] = "expression_energy_le";
//
//
//       DataTable dt = createDataTableFromFile(cols);
//        exportDataTableToCSV(dt);
//
//	    var thing = dt.determineDataTypes();
//
//        var lines = thing.Select(kvp => kvp.Key + ": " + kvp.Value.ToString());
//
//	    foreach (var VARIABLE in lines)
//	    {
//	        Debug.Log(VARIABLE);
//	    }
    }


    //TODO: REMOVE DEFAULT FILEPATH
    public DataTable createDataTableFromFile(string filePath)
    {
        IWorkbook workbook;
        using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            workbook = new HSSFWorkbook(stream);
        }

        ISheet sheet = workbook.GetSheetAt(0); // zero-based index of your target sheet
        DataTable dt = new DataTable(sheet.SheetName);

        // write header row & get headerIndexes
        IRow headerRow = sheet.GetRow(0);
        List<int> indexesOfHeaders = new List<int>();
        foreach (ICell headerCell in headerRow)
        {
            //if (columnStrings.Contains(headerCell.ToString()))
            //{
                dt.Columns.Add(headerCell.ToString());
                indexesOfHeaders.Add(headerCell.ColumnIndex);
                
          //  }
        }

        // write the rest
        int rowIndex = 0;
        foreach (IRow row in sheet)
        {
            // skip header row
            if (rowIndex++ == 0) continue;

            DataRow dataRow = dt.NewRow();
            List<string> valueArr = new List<string>();

            //grab only vars corresponding to colStrings
            foreach (var VARIABLE in indexesOfHeaders)
            {
                if (row.GetCell(VARIABLE, MissingCellPolicy.RETURN_NULL_AND_BLANK) == null)
                {
                    Debug.Log("Missing Value, are you sure you want this?");

                    valueArr.Add("");
                }
                else
                {
                    valueArr.Add(row.GetCell(VARIABLE, MissingCellPolicy.RETURN_NULL_AND_BLANK).ToString());
                }
            }


            dataRow.ItemArray = valueArr.ToArray();
            dt.Rows.Add(dataRow);
        }
        exportDataTableToCSV(dt);
        return dt;
    }


    public void exportDataTableToCSV(DataTable dt)
    {     
        var csvData = dt.ToCsv();
        File.WriteAllText("test.csv", csvData);
    }
    // Update is called once per frame
    void Update () {
		
	}



}
