using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Excel;
using Office = Microsoft.Office.Core;

using System.Windows.Forms;
using Exversion.Entities;
using Exversion.Utils;
using System.Security.Cryptography;

namespace Exversion.ExcelAddIn
{
    public static class ExcelUtils
    {
        private const string DOUBLE_QUOTE="\"";
        private static Excel.Workbook workbook;
        private static Excel.Worksheet worksheet;

        public static Excel.Application ExcelApp { private get; set; }
        public static ExcelDataset PreviewDataset { get; private set; }

        public static ExcelRangeState GetRangeState()
        {
            Excel.Range range=ExcelApp.Selection as Excel.Range;
            if (range == null)
                return ExcelRangeState.INVALID_SELECTION;
            else if (ExcelApp.WorksheetFunction.CountA(range) == 0)
                return ExcelRangeState.EMPTY_SELECTION;
            //else if (ExcelApp.WorksheetFunction.CountA(range.Rows[0]) < range.Columns.Count)
            //    return ExcelRangeState.UNCOMPLETE_COLUMNS;
            //else if (range.Columns.Count < 2 && range.Rows.Count < 3)
            //    return ExcelRangeState.TOO_SMALL_SELECTION;
            else
                return ExcelRangeState.READY;
        }

        public static string RangeToJSon(Excel.Range range)
        {
            StringBuilder result = new StringBuilder();
            StringBuilder line = new StringBuilder();
            bool firstLine = false;
            bool firstVal = false;
            dynamic val=null;

            if (PreviewDataset == null)
                PreviewDataset = new ExcelDataset();
            else
            {
                PreviewDataset.Rows.Clear();
                PreviewDataset.SelectedColumns.Clear();
            }

            result.Append("[");
            firstLine = true;

            Excel.Range firstRow = range.Rows[1];
            int rowCount = range.Rows.Count;
            int colCount= range.Columns.Count;
            string[] colNames = new string[colCount];
            string colName;
            Row row = null;

            for (int k = 0; k < colCount; k++)
            {
                colName = firstRow.Columns[k + 1].Cells[1].Text;
                colNames[k] = colName;
                PreviewDataset.SelectedColumns.Add(colName);
            }
            //foreach (Excel.Range row in range.Rows)
            for (int i = 2; i <= rowCount; i++)
            {
                if (ExcelApp.WorksheetFunction.CountA(range.Rows[i]) == 0)
                    continue;

                line.Clear();
                line.Append("{");
                firstVal = true;

                if (i <= Constants.MAX_PREVIEW_ROWS + 2)
                    row = new Row();
                //else
                //    row = null;

                for (int j = 1; j <= colCount; j++)
                {
                    var cell = range.Rows[i].Cells[j];
                    string text = cell.Text;//.ToString();
                    //if (row != null)
                    if (i <= Constants.MAX_PREVIEW_ROWS + 2)
                        row.Cells.Add(PreviewDataset.SelectedColumns[j - 1], text);
                    if (cell.Value != null)
                        val = cell.Value.ToString();
                    if (text != val)
                        val = DOUBLE_QUOTE + text.Replace(" ", "") + DOUBLE_QUOTE;
                    else
                        val = DOUBLE_QUOTE + text + DOUBLE_QUOTE;

                    val = DOUBLE_QUOTE + colNames[j-1] + DOUBLE_QUOTE + ":" + val;
                    if (firstVal)
                    {
                        line.Append(val);
                        firstVal = false;
                    }
                    else
                    {
                        line.Append("," + val);
                    }
                }
                PreviewDataset.Rows.Add(row);

                line.Append("}");
                if (firstLine)
                {
                    result.Append(line.ToString());
                    firstLine = false;
                }
                else
                {
                    result.Append("," + line.ToString());
                }
            }
            result.Append("]");
            
            return result.ToString();
        }
      
        public static Excel.CustomProperty GetCustomProperty(Excel.Worksheet ws, string propertyName)
        {
            foreach (Excel.CustomProperty p in ws.CustomProperties)
            {
                if (p.Name == propertyName)
                    return p;
            }

            return null;
        }

        public static void SetCustomProperty(Excel.Worksheet ws, string propertyName, string value)
        {
            Excel.CustomProperty p = GetCustomProperty(ws, propertyName);
            if (p != null)
                p.Value = value;
            else
                ws.CustomProperties.Add(propertyName, value);
        }

        public static Excel.Range NormalizeRange(Excel.Range rng)
        {
            
            bool finish = false;

            finish = false;
            int i;

            for (i = 1; i <= rng.Rows.Count; i++)
            {
                if (finish)
                    break; // TODO: might not be correct. Was : Exit For
                //If finish Or i = 1 Then Exit For
                if (Globals.ThisAddIn.Application.WorksheetFunction.CountA(rng.Rows[i]) == 0)
                {
                    rng = rng.Offset[1, 0].Resize[rng.Rows.Count - 1, rng.Columns.Count];
                    i -= 1;
                }
                else
                {
                    finish = true;
                }
            }

            finish = false;
            for (i = rng.Rows.Count; i >= 1; i += -1)
            {
                if (finish)
                    break; // TODO: might not be correct. Was : Exit For
                //If finish Or i = 1 Then Exit For
                if (Globals.ThisAddIn.Application.WorksheetFunction.CountA(rng.Rows[i]) == 0)
                {
                    rng = rng.Resize[rng.Rows.Count - 1, rng.Columns.Count];
                }
                else
                {
                    finish = true;
                }
            }

            finish = false;
            for (i = 1; i <= rng.Columns.Count; i++)
            {
                if (finish)
                    break; // TODO: might not be correct. Was : Exit For
                //If finish Or i = 1 Then Exit For
                if (Globals.ThisAddIn.Application.WorksheetFunction.CountA(rng.Columns[i]) == 0)
                {
                    rng = rng.Offset[0, 1].Resize[rng.Rows.Count, rng.Columns.Count - 1];
                    i -= 1;
                }
                else
                {
                    finish = true;
                }
            }

            finish = false;
            for (i = rng.Columns.Count; i >= 1; i += -1)
            {
                if (finish)
                    break; // TODO: might not be correct. Was : Exit For
                //If finish Or i = 1 Then Exit For
                if (Globals.ThisAddIn.Application.WorksheetFunction.CountA(rng.Columns[i]) == 0)
                {
                    rng = rng.Resize[rng.Rows.Count, rng.Columns.Count - 1];
                }
                else
                {
                    finish = true;
                }
            }

            return rng;

        }
        public static void ImportRange(ExcelDataset dataset,bool startFromCurrentCell=false,bool isTracked=true,bool isUpdate=false)
        {
            workbook = ExcelApp.ActiveWorkbook;
            worksheet = ExcelApp.ActiveSheet as Excel.Worksheet;

            if (!isUpdate )
            {
                if (startFromCurrentCell)
                {
                    try
                    {
                        Global.StartingRowIndex = ExcelApp.ActiveCell.Row+1;
                        Global.StartingColumnIndex = ExcelApp.ActiveCell.Column;
                    }
                    catch (Exception ex)
                    {
                        Exversion.Logging.Logger.LogEvent("Importing data to worksheet", ex.Message);
                    }
                }
                else
                {
                    Global.StartingRowIndex = 2;
                    Global.StartingColumnIndex = 1;
                }

                if (worksheet == null || ExcelApp.WorksheetFunction.CountA(worksheet.UsedRange) > 0)
                {
                    worksheet = ExcelApp.ActiveWorkbook.Worksheets.Add(
                        After: ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Worksheets.Count]);
                    if (!SheetExists(dataset.Name))
                        worksheet.Name = dataset.Name;
                }
            }
            
            worksheet.Application.ScreenUpdating = 
                worksheet.Application.DisplayAlerts = false;
            int offset=0;
            if (isTracked)
            {
                worksheet.Cells[Global.StartingRowIndex - 1, Global.StartingColumnIndex].Value = "ID";
                offset = 1;
            }

            Excel.Range cols = worksheet.Cells[Global.StartingRowIndex - 1, Global.StartingColumnIndex + offset];
            cols.Value = Utils.Converter.ListToString(dataset.SelectedColumns);
            cols.TextToColumns(Tab: true);

            int rowCount = dataset.Rows.Count;
            int colCount = dataset.SelectedColumns.Count + offset;
            Excel.Range range = worksheet.Range[worksheet.Cells[Global.StartingRowIndex, Global.StartingColumnIndex],
                                              worksheet.Cells[rowCount + Global.StartingRowIndex - 1,
                                                              colCount + Global.StartingColumnIndex - 1]];
            Excel.Range cell;
            Row curRow;
            double minWidth;
            try
            {
                minWidth = range.ColumnWidth;
            }
            catch { minWidth = 8.5; }

            int c;
            for (int i = 0; i < rowCount; i++)
            {
                curRow = dataset.Rows[i];
                //if (isTracked)
                //    worksheet.Cells[i + Global.StartingRowIndex, Global.StartingColumnIndex].Value = curRow.ID;
                cell = worksheet.Cells[i + Global.StartingRowIndex, Global.StartingColumnIndex + offset];
                cell.Value = curRow.Text;
                cell.TextToColumns(Tab: true);
                if (isTracked)
                {
                    worksheet.Cells[i + Global.StartingRowIndex, Global.StartingColumnIndex].Value = curRow.ID;
                    //curRow.Text = row.ToString();
                    //Recalculate row hash after putting data in excel which maybe get altered (autoformatted)
                    for (c = 0; c < dataset.SelectedColumns.Count; c++)
                        curRow.Cells[dataset.SelectedColumns[c]] = 
                            worksheet.Cells[i + Global.StartingRowIndex, Global.StartingColumnIndex + offset + c].Text.Trim();
                    
                    curRow.LocalHash = Security.HashSHA1(curRow.GetText());
                }
            }

            Excel.Range wholeRange = worksheet.Range[worksheet.Cells[Global.StartingRowIndex - 1, Global.StartingColumnIndex],
                                              worksheet.Cells[rowCount + Global.StartingRowIndex,
                                                              colCount + Global.StartingColumnIndex]];
            wholeRange.Columns.AutoFit();
            foreach (Excel.Range col in wholeRange.EntireColumn)
            {
                if (col.ColumnWidth < minWidth)
                    col.ColumnWidth = minWidth;
            }
            if (isTracked)
                wholeRange.Columns[1].Hidden = true;

            worksheet.Application.ScreenUpdating =
                worksheet.Application.DisplayAlerts = true;

            if(isTracked)
                AddRangeInfo(dataset, range);
        }

        private static void AddRangeInfo(ExcelDataset dataset, Excel.Range range)
        {
            string rangeName = GetValidRangeName(Constants.EXVERSION_DATASET + "_" + dataset.ID);
            range.Name = dataset.RangeName = rangeName;
            range.Select();
            Office.CustomXMLPart part = workbook.CustomXMLParts.Add(XMLSerializer.Serialize(dataset));
            Excel.CustomProperty p = GetCustomProperty(worksheet, Constants.EXVERSION_DATASET);
            if (p != null)
                p.Value = part.Id;
            else
                worksheet.CustomProperties.Add(Constants.EXVERSION_DATASET, part.Id);
        }

        private static string GetValidRangeName(string baseName)
        {
            int index = 0;
            string newName = baseName;
            bool exist;
            do
            {
                try
                {
                    exist = (workbook.Names.Item(newName) != null);
                }
                catch
                {
                    break;
                }
                index++;
                newName = baseName + "_" + index.ToString();
            } while (exist); 
            
            return newName;
        }

        private static bool SheetExists(string name)
        {
            foreach (dynamic sheet in ExcelApp.ActiveWorkbook.Worksheets)
            {
                if (sheet.Name == name)
                    return true;
            }

            return false;
        }
        private static bool isEmptySheet(Excel.Worksheet sheet)
        {
            bool t1=(sheet.UsedRange.Rows.Count == 1);
            bool t2=(sheet.UsedRange.Columns.Count == 1);
            bool t3 = (sheet.UsedRange[1, 1].Text == "");

            return  t1&& t2&& t3;
        }
    }

    public enum ExcelRangeState
    {
        INVALID_SELECTION = 0,
        EMPTY_SELECTION,
        TOO_SMALL_SELECTION,
        UNCOMPLETE_COLUMNS,
        READY
    }

    
}
