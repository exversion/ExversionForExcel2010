using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Exversion.Entities;
using System.Threading;

namespace Exversion.ExcelAddIn.Dialogs
{
    public partial class dlgFilterImportData : Form
    {
        DatasetInfo datasetInf;
        ExcelDataset dataset;
        private Dictionary<string, bool> columns;
        string query, filter,excludedColumns;
        bool isOwner;
        
        public dlgFilterImportData(DatasetInfo Dataset)
        {
            InitializeComponent();
            datasetInf = Dataset;
            dataset = new ExcelDataset();
            isOwner = Exversion.Globals.CurrentUser.Datasets.Exists(item => item.ID == Dataset.ID);
            chkTrackChanges.Visible = !isOwner;
            SetupGUI();
        }

        private void SetupGUI()
        {
            columns = new Dictionary<string, bool>();
            foreach (string col in datasetInf.Columns)
            {
                if(!columns.ContainsKey(col))
                    columns.Add(col, true);
            }
            UpdateColumns();

            lstOperator.Items.Add("  =  ");
            lstOperator.Items.Add(" < ? > ");
            lstOperator.Items.Add("  >=  ");
            lstOperator.Items.Add("  <=  ");

            lstColumns.SelectedIndex = lstOperator.SelectedIndex = 0;
            
            //txtMaxRecords.Text = Global.AppSettings.MaxRecords.ToString();
            lblTotalRows.Text = datasetInf.RowCount.ToString();
        }

        private void UpdateColumns()
        {
            excludedColumns = string.Empty;
            bool firsttime = true;

            lstColumns.Items.Clear();
            grdPreview.Rows.Clear();
            grdPreview.Columns.Clear();
            dataset.SelectedColumns.Clear();
            dataset.ExcludedColumns.Clear();

            cmdImport.Enabled = false;

            foreach (KeyValuePair<string, bool> col in columns)
            {
                if (col.Value == true)
                {
                    dataset.SelectedColumns.Add(col.Key);
                    lstColumns.Items.Add(col.Key);
                    grdPreview.Columns.Add(col.Key, col.Key);
                }
                else
                {
                    dataset.ExcludedColumns.Add(col.Key);
                    if (!firsttime)
                        excludedColumns += ",";
                    else
                        firsttime = false;

                    excludedColumns += col.Key;
                }
            }

            lstColumns.SelectedIndex = 0;
        }

        private void chkExcludeCols_CheckedChanged(object sender, EventArgs e)
        {
            cmdExcludeCols.Enabled = chkExcludeCols.Checked;
            if (!chkExcludeCols.Checked && excludedColumns!=string.Empty)
            {
                var keys = new List<string>(columns.Keys);
                foreach (string key in keys)
                {
                    columns[key] = true;
                }

                UpdateColumns();
            }
        }

        private void cmdExcludeCols_Click(object sender, EventArgs e)
        {
            dlgExcludeColumns dlg = new dlgExcludeColumns();
            dlg.Columns = columns;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                UpdateColumns();
                return;
            }
        }

        private void cmdPreview_Click(object sender, EventArgs e)
        {
            query = filter = string.Empty;
            filter = string.Format("&{0}=", lstColumns.SelectedItem.ToString());
            switch (lstOperator.SelectedIndex)
            {
                case 0:
                    filter += txtValue.Text;
                    break;
                case 1:
                    filter += string.Format("[{0}:{1}]", txtValue.Text, txtValue2.Text);
                    break;
                case 2:
                    filter += string.Format("[{0}:]", txtValue.Text);
                    break;
                case 3:
                    filter += string.Format("[:{0}]", txtValue.Text);
                    break;
            }
            query = filter + "&_limit=" + txtMaxRecords.Text;

            if (excludedColumns != string.Empty)
                query += "&_remove=" + excludedColumns;

            grdPreview.Rows.Clear();

            ExcelAddIn.Global.ProgressInfo = "Getting data, please wait..";
            ExcelAddIn.Global.ProgressIsFinished = false;
            dynamic raw = null;
            ThreadPool.QueueUserWorkItem(o =>
            {
                raw = ApiConsumer.GetRows(datasetInf.ID, query, true);
                ExcelAddIn.Global.ProgressIsFinished = true;
            });

            new dlgBusy().ShowDialog();

            if (raw != null)
            {
                List<string> selected, excluded;
                selected = dataset.SelectedColumns;
                excluded = dataset.ExcludedColumns;
                dataset = Utils.Converter.RawToExcelDataset(raw, selected);
                if (dataset != null && dataset.Rows.Count > 0)
                {
                    dataset.ID = datasetInf.ID;
                    dataset.Name = datasetInf.Name;
                    dataset.SelectedColumns = selected;
                    dataset.ExcludedColumns = excluded;
                    int count = (dataset.Rows.Count < Constants.MAX_PREVIEW_ROWS) ? dataset.Rows.Count : Constants.MAX_PREVIEW_ROWS;

                    grdPreview.Columns.Clear();
                    foreach (string key in dataset.Rows[0].Cells.Keys)
                        grdPreview.Columns.Add(key,key);

                    for (int i = 0; i < count; i++)
                    {
                        int index = grdPreview.Rows.Add(dataset.Rows[i].Cells.Values.ToArray());
                        grdPreview.Rows[index].HeaderCell.Value = (index + 1).ToString();
                    }
                    cmdImport.Enabled = true;
                    dataset.Query = query;
                    return;
                }
            }

            MessageBox.Show(ApiConsumer.LastErrorDescription, Constants.APP_NAME,
                MessageBoxButtons.OK, MessageBoxIcon.Question);
            cmdImport.Enabled = false;
        }

        private void lstOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtValue2.Visible = lblAnd.Visible = lblVal2.Visible = (lstOperator.SelectedIndex == 1);
        }

        private void cmdImport_Click(object sender, EventArgs e)
        {
            ExcelAddIn.Global.ProgressInfo = "Generating data in Excel, please wait..";
            ExcelAddIn.Global.ProgressIsFinished = false;
            dlgBusy dlg = new dlgBusy();
            ThreadPool.QueueUserWorkItem(o =>
            {
                dlg.ShowDialog();
            });

            if (isOwner)
                ExcelUtils.ImportRange(dataset, chkActiveCell.Checked);
            else
            {
                if (!chkTrackChanges.Checked)
                    ExcelUtils.ImportRange(dataset, chkActiveCell.Checked, false);
                else
                {
                    string forkedID = ApiConsumer.ForkDataset(datasetInf);
                    if (!string.IsNullOrEmpty(forkedID))
                    {
                        dataset.ID = forkedID;
                        ExcelUtils.ImportRange(dataset, chkActiveCell.Checked);
                    }
                    else
                    {
                        dlg.BeginInvoke(new MethodInvoker(dlg.Close));
                        MessageBox.Show("Cannot create a branch for this dataset!\nDetails:\n" +
                                        ApiConsumer.LastErrorDescription, Constants.APP_NAME,
                                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }
            
            dlg.BeginInvoke(new MethodInvoker(dlg.Close));
            DialogResult = DialogResult.OK;
        }
    }
}
