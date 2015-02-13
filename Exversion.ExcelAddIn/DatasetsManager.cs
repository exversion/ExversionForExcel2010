using Exversion.Entities;
using Exversion.Logging;
using Exversion.Utils;
//using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace Exversion.ExcelAddIn
{
    static class DatasetsManager
    {
        private static Excel.Range updatedRange;
        //private static List<WorkbookDatasets> availableDatasets;
        private static Dictionary<string, List<ExcelDataset>> availableDatasets;

        static DatasetsManager()
        {
            availableDatasets = new Dictionary<string, List<ExcelDataset>>();//List<WorkbookDatasets>();
        }

        public static void AddDataset(string WorkbookFullName, ExcelDataset dataset)
        {
            //availableDatasets[WorkbookFullName].Add(dataset);
        }
        public static void RemoveDataset(Excel.Workbook wb, string ID)
        {
            ExcelDataset dataset = availableDatasets[wb.FullName].Find(d => d.ID == ID);
            if (dataset != null)
            {
                availableDatasets[wb.FullName].Remove(dataset);
                foreach (Excel.Worksheet ws in wb.Worksheets)
                {
                    Excel.CustomProperty p = ExcelUtils.GetCustomProperty(ws, Constants.EXVERSION_DATASET);
                    if (p != null)
                    {
                        if (!string.IsNullOrEmpty(p.Value))
                        {
                            Office.CustomXMLPart part = wb.CustomXMLParts.SelectByID(p.Value);
                            if (part != null)
                                part.Delete();
                        }

                        p.Delete();
                    }
                }
            }
        }

        public static bool TryAddDatasets(Excel.Workbook wb)
        {
            string key = wb.FullName;

            List<ExcelDataset> datasets = new List<ExcelDataset>();

            foreach (Excel.Worksheet sheet in wb.Worksheets)
            {
                ExcelDataset dataset = GetAssociatedDataset(sheet);
                if (dataset != null)
                    datasets.Add(dataset);
            }

            if (datasets.Count > 0)
            {
                if (!availableDatasets.ContainsKey(key))
                    availableDatasets.Add(key, null);

                availableDatasets[key] = datasets;
                return true;
            }

            return false;
        }

        public static ExcelDataset GetAssociatedDataset(Excel.Worksheet worksheet)
        {
            Office.CustomXMLPart part = GetAssociatedXMLPart(worksheet, Constants.EXVERSION_DATASET);
            if (part != null && !string.IsNullOrEmpty(part.XML))
            {
                try
                {
                    ExcelDataset dataset = XMLSerializer.Deserialize<ExcelDataset>(part.XML);
                    dataset.AssociatedWorksheet = worksheet;
                    return dataset;
                }
                catch (Exception ex)
                {
                    Logger.LogEvent("Extracting worksheet's associated dataset", ex.Message);
                }

            }
            return null;
        }

        public static Office.CustomXMLPart GetAssociatedXMLPart(Excel.Worksheet ws, string usingProperty)
        {
            Excel.CustomProperty p = ExcelUtils.GetCustomProperty(ws, usingProperty);
            if (p != null && !string.IsNullOrEmpty(p.Value))
            {
                Excel.Workbook wb = ws.Parent as Excel.Workbook;
                if (wb.CustomXMLParts != null)
                {
                    return wb.CustomXMLParts.SelectByID(p.Value);
                }
            }
            return null;
        }

        public static DatasetUpdate GetRemoteUpdate(ExcelDataset curDataset)
        {
            DatasetInfo datasetInf = ApiConsumer.GetMetaData(curDataset.ID);
            if (datasetInf != null)
            {
                if (datasetInf.Columns.Length > 0 && datasetInf.RowCount > 0)
                {
                    var raw = ApiConsumer.GetRows(curDataset.ID, curDataset.Query);
                    if (raw != null)
                    {
                        //ExcelDataset newDataset = Converter.RawToExcelDataset(raw,null);
                        List<string> columns = datasetInf.Columns.ToList();
                        ExcelDataset newDataset = Converter.RawToExcelDataset(raw, columns);
                        if (newDataset != null && newDataset.Rows.Count > 0)
                        {
                            newDataset.SelectedColumns = columns;
                            DatasetUpdate update = new DatasetUpdate();
                            List<string> newColumns = new List<string>(newDataset.SelectedColumns);
                            List<string> oldColumns = new List<string>(curDataset.SelectedColumns);
                            newColumns.Sort();
                            oldColumns.Sort();

                            if (!newColumns.SequenceEqual(oldColumns))
                            {
                                update.ColumnsList = newDataset.SelectedColumns;
                                update.AddedRows = newDataset.Rows;
                            }
                            else
                            {
                                update.AddedRows = newDataset.Rows.Except(curDataset.Rows,
                                                                              new RowComparers.IDComparer()
                                                                              ).ToList();
                                update.EditedRows = newDataset.Rows.Except(curDataset.Rows,
                                                                          new RowComparers.RowComparer()
                                                                          ).ToList().Except(update.AddedRows).ToList();
                                update.RemovedRows = curDataset.Rows.Except(newDataset.Rows,
                                                                           new RowComparers.IDComparer()
                                                                           ).ToList();
                            }
                            return update;
                        }
                    }
                }
            }
            return null;
        }

        public static void UpdateLocal(ExcelDataset oldDataset, DatasetUpdate update)
        {
            Excel.Worksheet sheet = ((Excel.Worksheet)oldDataset.AssociatedWorksheet);
            Excel.Range range = sheet.Range[oldDataset.RangeName];
            Office.CustomXMLPart part = GetAssociatedXMLPart(sheet, Constants.EXVERSION_DATASET);
            Office.CustomXMLNode node;
            Row oldRow;
            int index, rowsCount;

            sheet.Application.ScreenUpdating = 
                sheet.Application.DisplayAlerts = false;
            
            if (update.ColumnsList != null)
            {
                oldDataset.Rows = update.AddedRows;
                oldDataset.SelectedColumns = update.ColumnsList;
                range.Clear();
                int rowIndex = range.Row;
                int columnIndex=range.Column;
                Global.StartingRowIndex = rowIndex;
                Global.StartingColumnIndex = columnIndex;

                sheet.Range[sheet.Cells[rowIndex-1, columnIndex],
                            sheet.Cells[rowIndex-1, columnIndex + range.Columns.Count]].Clear();
                part.Delete();
                range.Name.Delete();
                ExcelUtils.ImportRange(oldDataset, true);
            }
            else
            {
                foreach (Row row in update.EditedRows)
                {
                    oldRow = oldDataset.Rows.Find(r => r.ID == row.ID);
                    if (oldRow != null)
                    {
                        index = oldDataset.Rows.IndexOf(oldRow) + 1;
                        range.Rows[index].Cells[2].Value = row.ToString();
                        range.Rows[index].Cells[2].TextToColumns(Tab: true);
                        row.LocalHash = BuildRowFromRange(range.Rows[index].Cells[2], oldDataset.SelectedColumns.Count, oldDataset.SelectedColumns).LocalHash;

                        node = part.SelectSingleNode("//Rows/Row[@ID=\"" + oldRow.ID + "\"]");
                        oldRow.LocalHash = node.SelectSingleNode("./@LocalHash").Text = row.LocalHash;
                        oldRow.RemoteHash = node.SelectSingleNode("./@RemoteHash").Text = row.RemoteHash;
                    }
                }

                rowsCount = range.Rows.Count;
                if (update.AddedRows.Count > 0)
                {
                    for (int i = 0; i < update.AddedRows.Count; i++)
                    {
                        range.Rows[rowsCount + 1].Insert(Excel.XlInsertShiftDirection.xlShiftDown);
                        range.Rows[rowsCount + 1].Cells[1].Value = update.AddedRows[i].ID;
                        range.Rows[rowsCount + 1].Cells[2].Value = update.AddedRows[i].ToString();
                        range.Rows[rowsCount + 1].Cells[2].TextToColumns(Tab: true);
                        update.AddedRows[i].LocalHash = BuildRowFromRange(range.Rows[rowsCount + 1].Cells[2], oldDataset.SelectedColumns.Count, oldDataset.SelectedColumns).LocalHash;

                        node = part.SelectSingleNode("//Rows");
                        node.AppendChildSubtree(string.Format("<Row ID=\"{0}\" LocalHash=\"{1}\" RemoteHash=\"{2}\"/>",
                            update.AddedRows[i].ID, update.AddedRows[i].LocalHash, update.AddedRows[i].RemoteHash));

                        oldDataset.Rows.Add(update.AddedRows[i]);

                        rowsCount++;
                    }

                    Excel.Range newrange = range.Resize[rowsCount, range.Columns.Count];
                    newrange.Name = oldDataset.RangeName;
                }

                if (update.RemovedRows.Count > 0)
                {
                    for (int i = 0; i < update.RemovedRows.Count; i++)
                    {
                        oldRow = oldDataset.Rows.Find(r => r.ID == update.RemovedRows[i].ID);
                        if (oldRow != null)
                        {
                            node = part.SelectSingleNode("//Rows/Row[@ID=\"" + oldRow.ID + "\"]");
                            node.Delete();

                            index = oldDataset.Rows.IndexOf(oldRow);
                            oldDataset.Rows.RemoveAt(index);
                            range.Rows[index + 1].Delete();
                        }
                    }
                }
            }
            
            sheet.Application.ScreenUpdating = 
                sheet.Application.DisplayAlerts = true;
        }

        public static DatasetUpdate GetLocalUpdate(ExcelDataset curDataset)
        {
            Excel.Worksheet sheet = curDataset.AssociatedWorksheet as Excel.Worksheet;
            Excel.Application excel = sheet.Application;
            Excel.Range range = sheet.Range[curDataset.RangeName];

            DatasetUpdate update = new DatasetUpdate();

            int cellsCount;
            List<string> curColumns =
                BuildHeaderFromCells(sheet.Cells[range.Row - 1, range.Column+1]);

            if (!curColumns.SequenceEqual(curDataset.SelectedColumns))
            {
                update.ColumnsList = curColumns;
                update.AddedColumns = curColumns.Except(curDataset.SelectedColumns).ToList();
                update.RemovedColumns = curDataset.SelectedColumns.Except(curColumns).ToList();
                cellsCount = curColumns.Count;
            }
            else
                cellsCount = curDataset.SelectedColumns.Count;

            update.ExistingRows = new List<Row>();
            update.EditedRows = new List<Row>();
            update.AddedRows = new List<Row>();
            Row row;
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                row = BuildRowFromRange(range.Rows[i], cellsCount, curDataset.SelectedColumns);
                if (row != null)
                {
                    if (!string.IsNullOrEmpty(row.ID))
                    {
                        update.ExistingRows.Add(row);
                        if (curDataset.Rows.Exists(r => r.ID == row.ID && r.LocalHash != row.LocalHash))
                        {
                            update.EditedRows.Add(row);
                        }
                    }
                    else
                    {
                        row.IndexInRange = i;
                        update.AddedRows.Add(row);
                    }
                }
            }
            update.RemovedRows = curDataset.Rows.Except(update.ExistingRows,
                                                        new RowComparers.IDComparer()
                                                        ).ToList();

            bool finish=false;
            updatedRange = null;
            int count;
            do
            {
                count = range.Rows.Count + 1;
                range = range.Resize[count, cellsCount];
                row = BuildRowFromRange(range.Rows[range.Rows.Count], cellsCount, curDataset.SelectedColumns);
                if (row != null)
                {
                    row.IndexInRange = count;
                    update.AddedRows.Add(row);
                }
                else
                {
                    finish = true;
                }

            } while (finish == false);

            updatedRange = range.Resize[count - 1, cellsCount];

            return update;
        }

        public static bool UpdateRemote(ExcelDataset dataset, DatasetUpdate update)
        {
            Excel.Worksheet sheet = ((Excel.Worksheet)dataset.AssociatedWorksheet);
            Excel.Range range = sheet.Range[dataset.RangeName];
            Office.CustomXMLPart part = GetAssociatedXMLPart(sheet, Constants.EXVERSION_DATASET);
            Office.CustomXMLNode node;

            if (update.ColumnsList != null)
            {
                if (update.AddedColumns.Count > 0 || update.RemovedColumns.Count > 0)
                {
                    if (ApiConsumer.AlterColumns(dataset.ID, update.AddedColumns, update.RemovedColumns))
                    {
                        dataset.SelectedColumns = update.ColumnsList;
                        node = part.SelectSingleNode("//SelectedColumns");
                        foreach (Office.CustomXMLNode column in node.ChildNodes)
                            column.Delete();
                        foreach (string column in dataset.SelectedColumns)
                            node.AppendChildNode(Name:"string", NodeValue: column);
                    }
                }
                dataset.Rows = update.ExistingRows;
            }else
                dataset.Rows = update.EditedRows;

            if (dataset.Rows.Count > 0)
            {
                if (ApiConsumer.EditData(dataset))
                {
                    foreach (Row row in dataset.Rows)
                    {
                        row.RemoteHash = row.LocalHash;
                        node = part.SelectSingleNode("//Rows/Row[@ID=\"" + row.ID + "\"]");
                        node.SelectSingleNode("./@LocalHash").Text = row.LocalHash;
                        node.SelectSingleNode("./@RemoteHash").Text = row.RemoteHash;
                    }
                }
            }

            if (update.AddedRows.Count > 0)
            {
                updatedRange.Name = dataset.RangeName;
                dataset.Rows = update.AddedRows;
                PushedDataResults results=ApiConsumer.PushData(dataset);
                if (results != null)
                {
                    node = part.SelectSingleNode("//Rows");
                    ProcessAddedRows(results,dataset,node,range);
                }
            }

            if (update.RemovedRows.Count > 0)
            {
                dataset.Rows = update.RemovedRows;
                if (ApiConsumer.DeleteData(dataset))
                {
                    foreach (Row row in dataset.Rows)
                    {
                        node = part.SelectSingleNode("//Rows/Row[@ID=\"" + row.ID + "\"]");
                        node.Delete();
                    }
                }
            }

            return true;
        }

        private static void ProcessAddedRows(PushedDataResults results, ExcelDataset dataset, Office.CustomXMLNode node, Excel.Range range)
        {

            List<Row> rows;
            if (results.InsertedRows != null)
            {
                rows = BuildRowsFromArray(results.InsertedRows);
                if(rows!=null)
                    AddRows(rows,dataset, node, range);
            }

            if (results.UpdatedRows!= null)
            {
                rows = BuildRowsFromArray(results.UpdatedRows);
                if (rows != null)
                    AddRows(rows, dataset, node, range);
            }
        }
        private static void AddRows(List<Row> rows,ExcelDataset dataset, Office.CustomXMLNode node, Excel.Range range)
        {
            Row row;
            foreach (Row item in rows)
            {
                row = dataset.Rows.Find(r => r.Text == item.Text);
                if (row != null)
                {
                    node.AppendChildSubtree(
                    string.Format("<Row RemoteHash=\"{0}\" LocalHash=\"{1}\" ID=\"{2}\"/>",
                    row.RemoteHash,row.LocalHash, item.ID));

                    range.Rows[row.IndexInRange].Cells[1].Value = item.ID;
                }
            }
        }

        private static List<Row> BuildRowsFromArray(Array array)
        {
            if (array.Length > 0)
            {
                List<Row> rows = new List<Row>();
                foreach (var item in array)
                {
                    Row row = BuildRowFromArray(item);
                    if (row != null)
                        rows.Add(row);
                }

                if (rows.Count > 0)
                    return rows;
            }
            return null;
        }

        private static Row BuildRowFromArray(dynamic array)
        {
            Row row = new Row()
            {
                ID = array["_id"].Value.ToString()
            };

            foreach (var value in array)
            {
                string val = value.Name.ToString();
                if (val != "_hash")
                {
                    if (val != "_id")
                        row.Cells.Add(value.Key,value.Value.ToString());
                }
                else
                {
                    break;
                }
            }

            row.Text = row.ToString();
            return row;
        }

        private static Row BuildRowFromRange(Excel.Range range, int cellsCount, List<string> colNames)
        {
            if (range.Application.WorksheetFunction.CountA(range) > 0)
            {
                Row row = new Row();
                row.ID = range.Cells[1].Text;
                Excel.Range cell = range.Cells[1];
                for (int i = 2; i <= cellsCount + 1; i++)
                {
                    cell = cell.Offset[0, 1];
                    row.Cells.Add(colNames[i-2], cell.Text.Trim());
                }
             
                row.LocalHash = Utils.Security.HashSHA1(row.GetText());

                return row;
            }
            return null;
        }

        private static List<string> BuildHeaderFromCells(Excel.Range startingCell)
        {
            List<string> columns = new List<string>();

            while (!string.IsNullOrWhiteSpace(startingCell.Text))
            {
                columns.Add(startingCell.Text);
                startingCell=startingCell.Offset[0, 1];
            }
            return columns;
        }
    }
    public class DatasetUpdate
    {
        public bool ColumnsOrderChanged { get; set; }

        public List<string> ColumnsList { get; set; }
        public List<string> AddedColumns { get; set; }
        public List<string> RemovedColumns { get; set; }

        public List<Row> ExistingRows { get; set; }
        public List<Row> EditedRows { get; set; }
        public List<Row> AddedRows { get; set; }
        public List<Row> RemovedRows { get; set; }
    }
}
