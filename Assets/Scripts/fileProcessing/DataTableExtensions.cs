using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public static class DataTableExtensions
    {
        public static string ToCsv(this DataTable dataTable)
        {
            StringBuilder sbData = new StringBuilder();

            // Only return Null if there is no structure.
            if (dataTable.Columns.Count == 0)
                return null;

            foreach (var col in dataTable.Columns)
            {
                if (col == null)
                    sbData.Append(",");
                else
                    sbData.Append("\"" + col.ToString().Replace("\"", "\"\"") + "\",");
            }

            sbData.Replace(",", System.Environment.NewLine, sbData.Length - 1, 1);

            foreach (DataRow dr in dataTable.Rows)
            {
                foreach (var column in dr.ItemArray)
                {
                    if (column == null)
                        sbData.Append(",");
                    else
                        sbData.Append("\"" + column.ToString().Replace("\"", "\"\"") + "\",");
                }
                sbData.Replace(",", System.Environment.NewLine, sbData.Length - 1, 1);
            }

            return sbData.ToString();
        }

        public static Dictionary<string, string> determineDataTypes(this DataTable dataTable)
        {
            UnityEngine.Debug.Log("length: "+dataTable.Rows[1].ItemArray.Length);
            Dictionary<string,string> columnDataTypes = new Dictionary<string, string>();
            for (int i = 0;  i<dataTable.Rows[1].ItemArray.Length; i++)
            {
                int tempInt;
                float tempFloat;

                if (int.TryParse(dataTable.Rows[1][i].ToString(), out tempInt))
                {
                    columnDataTypes.Add(dataTable.Columns[i].ToString(), "int");
                //   UnityEngine.Debug.Log("Parsed to int");
                }

                else if (float.TryParse(dataTable.Rows[1][i].ToString(), out tempFloat))
                {
                    columnDataTypes.Add(dataTable.Columns[i].ToString(), "float");
                    //    UnityEngine.Debug.Log("Parsed to float");


                }

                else
                {
                    columnDataTypes.Add(dataTable.Columns[i].ToString(), "string");
                 //   UnityEngine.Debug.Log("Parsed to string");

                }
            }

          

            return columnDataTypes;


        }

     
    }
}



