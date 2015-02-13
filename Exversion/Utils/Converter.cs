using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Exversion.Entities;

namespace Exversion.Utils
{
    public static class Converter
    {
        public static ExcelDataset RawToExcelDataset(List<Dictionary<string, dynamic>> raw, List<string> columns)
        {
            if (raw != null)
            {
                ExcelDataset dataset = new ExcelDataset();
                if (columns == null)
                {
                    foreach (string key in raw[0].Keys)
                    {
                        if(key!=Constants.Strings.ID && key!=Constants.Strings.HASH)
                            dataset.SelectedColumns.Add(key);
                    }
                    columns = dataset.SelectedColumns;
                }

                foreach (Dictionary<string, dynamic> dic in raw)
                {
                    Row row = new Row()
                    {
                        ID = dic[Constants.Strings.ID]
                    };

                    foreach (string col in columns)
                    {
                        if (dic.ContainsKey(col))
                            row.Cells.Add(col, dic[col]);
                        else
                            row.Cells.Add(col, string.Empty);
                    }
                    row.Text = row.ToString();
                    row.RemoteHash = Security.HashSHA1(row.GetText());
                    dataset.Rows.Add(row);
                }
                return dataset;
            }
            else
                return null;
        }

        public static string DictionaryToString(Dictionary<string, dynamic> dic, bool sort = false)
        {
            string result = string.Empty;
            bool firsttime = true;
            List<string> columns = dic.Keys.ToList();
            
            if(sort)
                columns.Sort();

            foreach (string col in columns)
            {
                if (firsttime)
                    firsttime = !firsttime;
                else
                    result += "\t";
                result += dic[col];
            }
            return result;
        }
        //public static ExcelDataset RawToExcelDataset(List<Dictionary<string, string>> raw)
        //{
        //    if (raw != null)
        //    {
        //        ExcelDataset dataset = new ExcelDataset();
        //        foreach (string key in raw[0].Keys)
        //            dataset.SelectedColumns.Add(key);
        //        dataset.SelectedColumns.Remove(Constants.Strings.ID);

        //        foreach (Dictionary<string, string> line in raw)
        //        {
        //            Row row = new Row() 
        //            { 
        //                ID = line[Constants.Strings.ID]
        //            };

        //            foreach (var cell in line)
        //            {
        //                if (cell.Key != Constants.Strings.ID && cell.Key != Constants.Strings.HASH)
        //                    row.Cells.Add(cell.Value);
        //            }

        //            row.Text = row.ToString();
        //            row.Hash = Hash.SHA1(row.Text);
        //            dataset.Rows.Add(row);
        //        }
        //        return dataset;
        //    }
        //    else
        //        return null;
        //}

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
