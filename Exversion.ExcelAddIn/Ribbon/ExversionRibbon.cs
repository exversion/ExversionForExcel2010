using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Ribbon;

using Exversion.ExcelAddIn.Dialogs;
using System.Windows.Forms;
using Exversion.Entities;

namespace Exversion.ExcelAddIn
{
    public partial class ExversionRibbon
    {
        private void ExversionRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, RibbonControlEventArgs e)
        {
            if (Exversion.ExcelAddIn.Global.AppSettings.IsUserLoggged)
            {
                Exversion.ExcelAddIn.Global.AppSettings.IsUserLoggged = false;
                Settings.SaveSettings(Exversion.ExcelAddIn.Global.AppSettings);
                Exversion.ExcelAddIn.Global.OnSessionLogged(false);
            }else
                new dlgLogin().ShowDialog();
        }

        private void btnExport_Click(object sender, RibbonControlEventArgs e)
        {
            string JsonData = GetJsonData();
            if (JsonData != null && ExcelUtils.PreviewDataset != null &&
                ExcelUtils.PreviewDataset.SelectedColumns.Count > 0 && ExcelUtils.PreviewDataset.Rows.Count > 0)
                new dlgExportData(JsonData).ShowDialog();
        }

        private string GetJsonData()
        {
            string msg = null;

            if (IsRangeReady())
            {
                var range = ExcelUtils.NormalizeRange(Globals.ThisAddIn.Application.Selection);
                if (Globals.ThisAddIn.Application.WorksheetFunction.CountA(range.Rows[1]) < range.Columns.Count)
                    msg = "The first row, which represents columns names, should not contain any empty cells!";
                else if (range.Columns.Count < 2|| range.Rows.Count < 3)
                    msg = "You MUST select at least a 2x3 range!";
                else
                {
                    range.Select();
                    return ExcelUtils.RangeToJSon(range);
                }
                MessageBox.Show(msg, Constants.APP_NAME,
                        MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

            return null;
        }
        private bool IsRangeReady()
        {
            ExcelRangeState state = ExcelUtils.GetRangeState();
            string msg;

            switch (state)
            {
                case ExcelRangeState.READY:
                    return true;
                //case ExcelRangeState.EMPTY_SELECTION:
                //    msg = "";
                //    break;
                default:
                    msg = "Please, select a range!";
                    break;
            }

            MessageBox.Show(msg, Constants.APP_NAME,
                MessageBoxButtons.OK, MessageBoxIcon.Question);
            return false;

        }

        private void btnImportMyData_Click(object sender, RibbonControlEventArgs e)
        {
            if (Exversion.Globals.CurrentUser.Datasets != null &&
                Exversion.Globals.CurrentUser.Datasets.Count > 0)
            {
                new dlgSelectData().ShowDialog();
            }
            else
            {
                MessageBox.Show("No datasets are available on your profile!",
                    Constants.APP_NAME,MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void btnSearchImport_Click(object sender, RibbonControlEventArgs e)
        {
            new dlgSearchData().ShowDialog();
        }
        private void btnUpdateLocal_Click(object sender, RibbonControlEventArgs e)
        {
            ExcelDataset dataset =
                DatasetsManager.GetAssociatedDataset((Excel.Worksheet)Globals.ThisAddIn.Application.ActiveSheet);
            if (dataset != null)
            {
                DatasetUpdate update = DatasetsManager.GetRemoteUpdate(dataset);
                if (update != null && (update.ColumnsList != null ||
                    (update.EditedRows.Count > 0 ||update.AddedRows.Count>0 || update.RemovedRows.Count>0)))
                {
                    DatasetsManager.UpdateLocal(dataset, update);
                    MessageBox.Show("The local dataset has been updated.",
                        Constants.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //update.EditedRows.Count + " row(s) edited.\n" +
                        //update.AddedRows.Count + " row(s) added.\n" +
                        //update.RemovedRows.Count + " row(s) removed.",
                    
                }
                else
                {
                    MessageBox.Show("No pending changes on the remote dataset.",
                    Constants.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No dataset is available in this worksheet or,\n" + 
                    "The data in this worksheet is not linked to a repository you have permission to edit.",
                    Constants.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void btnUpdateRemote_Click(object sender, RibbonControlEventArgs e)
        {
            ExcelDataset dataset =
                DatasetsManager.GetAssociatedDataset((Excel.Worksheet)Globals.ThisAddIn.Application.ActiveSheet);
            if (dataset != null)
            {
                DatasetUpdate update = DatasetsManager.GetLocalUpdate(dataset);
                if (update != null && (update.ColumnsList != null ||
                    (update.EditedRows.Count > 0 || update.AddedRows.Count > 0 || update.RemovedRows.Count > 0)))
                {
                    int removed = update.RemovedRows.Count;
                    DatasetsManager.UpdateRemote(dataset, update);
                    MessageBox.Show("The remote dataset has been updated:\n" +
                        update.EditedRows.Count + " row(s) edited.\n" +
                        update.AddedRows.Count + " row(s) added.\n" +
                        removed + " row(s) removed.",
                    Constants.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No pending changes on the local dataset.",
                    Constants.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No dataset is available in this worksheet or,\n" +
                   "The data in this worksheet is not linked to a repository you have permission to edit.",
                   Constants.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void btnHelp_Click(object sender, RibbonControlEventArgs e)
        {
            System.Diagnostics.Process.Start("https://exversion.com/docs/");
        }

        private void btnAbout_Click(object sender, RibbonControlEventArgs e)
        {
            new dlgAbout().ShowDialog();
        }

        private void btnSelectDataset_Click(object sender, RibbonControlEventArgs e)
        {
            ExcelDataset dataset =
                DatasetsManager.GetAssociatedDataset(Globals.ThisAddIn.Application.ActiveSheet as Excel.Worksheet);
            if (dataset != null)
            {
                Globals.ThisAddIn.Application.Range[dataset.RangeName].Select();
            }
            else
            {
                MessageBox.Show("No dataset is available in this worksheet or,\n" +
                   "The data in this worksheet is not linked to a repository you have permission to edit.",
                   Constants.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        
    }
}
