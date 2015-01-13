using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Exversion.Entities;

namespace Exversion.ExcelAddIn.Dialogs
{
    public partial class dlgExportData : Form
    {
        private string JsonData;
        public dlgExportData(string jsonData)
        {
            InitializeComponent();
            JsonData = jsonData;

            foreach (string col in ExcelUtils.PreviewDataset.SelectedColumns)
            {
                dtGridPreview.Columns.Add(col, col);
            }

            int maxRows = (ExcelUtils.PreviewDataset.Rows.Count < Constants.MAX_PREVIEW_ROWS) ? 
                ExcelUtils.PreviewDataset.Rows.Count : Constants.MAX_PREVIEW_ROWS;

            for (int i = 0; i < maxRows; i++)
            {
                int index = dtGridPreview.Rows.Add(ExcelUtils.PreviewDataset.Rows[i].Cells.ToArray());
                dtGridPreview.Rows[index].HeaderCell.Value = (index + 1).ToString();
            }
        }

        private void cmdExport_Click(object sender, EventArgs e)
        {
            DatasetInfo dataset;

            dataset = new DatasetInfo()
                {
                    Name = txtTitle.Text,
                    Description = txtDesc.Text,
                    Private = Convert.ToInt32(chkPrivate.Checked),
                    SourceAuthor = Exversion.Globals.CurrentUser.UserName,
                    SourceUrl = Exversion.Constants.URLs.BASE_URL,
                    Organization = 0,
                    SourceDate = DateTime.Now.ToShortDateString(),
                };

                if (!ApiConsumer.CreateDataset(dataset))
                {
                    MessageBox.Show("Dataset creation failed!\nDetails:\n" +
                        ApiConsumer.LastErrorDescription, Constants.APP_NAME,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

            dataset.JsonData = JsonData;
            ExportData(dataset);
        }

        private void ExportData(DatasetInfo dataset)
        {
            if (ApiConsumer.PushData(dataset))
            {
                MessageBox.Show("Data has been exported successfully.", Constants.APP_NAME,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Data could not be pushed!\nDetails:\n" +
                ApiConsumer.LastErrorDescription, Constants.APP_NAME,
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
    }
}
