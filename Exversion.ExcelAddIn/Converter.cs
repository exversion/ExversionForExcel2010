using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Exversion.Entities;

namespace Exversion.ExcelAddIn
{
    static class Converter
    {
        public static ExcelDataset RawToExcelDataset(dynamic raw, string[] header)
        {
            if (raw != null)
            {
                ExcelDataset dataset = new ExcelDataset();
                if (header != null && header.Length > 0)
                {
                    dataset.Columns = new List<string>();
                    foreach (var column in header)
                    {
                        dataset.Columns.Add(column);
                    }
                }
                
                foreach (var line in raw)
                {
                    Row row = new Row() 
                    { 
                        ID = line["_id"].Value.ToString() 
                    };

                    foreach (var value in line)
                    {
                        if(value.Name.ToString()!="_id")
                            row.Cells.Add(value.Value.ToString());
                    }

                    row.Text = row.ToString();
                    row.RemoteHash = Utils.Hash.SHA1(row.Text);
                    dataset.Rows.Add(row);
                }
                return dataset;
            }
            else
                return null;
        }

        public static string ListToString(List<string> list)
        {
            string result = string.Empty;

            bool firsttime = true;
            foreach (string cell in list)
            {
                if (firsttime)
                    firsttime = !firsttime;
                else
                    result += "\t";
                result += cell;
            }
            return result;
        }
    }
}
