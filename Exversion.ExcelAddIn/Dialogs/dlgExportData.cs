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
    public partial class dlgExportData : Form
    {
        private string JsonData;
        public dlgExportData(string jsonData)
        {
            InitializeComponent();
            JsonData = jsonData;

            txtPreview.Text = Global.PreviewJSON;
        }

        private void cmdExport_Click(object sender, EventArgs e)
        {
           DatasetInfo dataset = new DatasetInfo()
                {
                    Name = txtTitle.Text,
                    Description = txtDesc.Text,
                    Private = Convert.ToInt32(chkPrivate.Checked),
                    SourceAuthor = Exversion.Globals.CurrentUser.UserName,
                    SourceUrl = Exversion.Constants.URLs.BASE_URL,
                    Organization = 0,
                    SourceDate = DateTime.Now.ToShortDateString(),
                };

           ExcelAddIn.Global.ProgressInfo = "Creating remote dataset, please wait..";
           ExcelAddIn.Global.ProgressIsFinished = false;
           bool wasDatasetCreated, wasDataPushed;
           wasDatasetCreated = wasDataPushed = false;

           //dlgBusy dlg = new dlgBusy();
            ThreadPool.QueueUserWorkItem(o =>
            {
                wasDatasetCreated = ApiConsumer.CreateDataset(dataset);
                if (wasDatasetCreated)
                {
                    ExcelAddIn.Global.ProgressInfo = "Uploading data, please wait..";
                    dataset.JsonData = JsonData;
                    wasDataPushed = ApiConsumer.PushData(dataset);
                }
                ExcelAddIn.Global.ProgressIsFinished = true;
            });

            new dlgBusy().ShowDialog();

            if (!wasDatasetCreated)
            {
                MessageBox.Show("Dataset creation failed!\nDetails:\n" +
                    ApiConsumer.LastErrorDescription, Constants.APP_NAME,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (wasDataPushed)
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
