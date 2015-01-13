using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Exversion.API;
using Exversion.Entities;
using Exversion.Utils;


namespace Exversion.ExcelAddIn.Dialogs
{
    public partial class dlgCreateDataset : Form
    {
        private string JsonData;

        public dlgCreateDataset(string jsonData)
        {
            InitializeComponent();
            JsonData = jsonData;
        }

        private void cmdCreate_Click(object sender, EventArgs e)
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

            if (ApiConsumer.CreateDataset(dataset))
            {
                dataset.JsonData = JsonData;
                if (ApiConsumer.PushData(dataset))
                {
                    MessageBox.Show("Data has been pushed successfully to the server." +
                    ApiConsumer.LastErrorDescription, Constants.APP_NAME,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Data could not be pushed!\nDetails:\n" +
                    ApiConsumer.LastErrorDescription, Constants.APP_NAME,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Dataset creation failed!\nDetails:\n" +
                    ApiConsumer.LastErrorDescription, Constants.APP_NAME,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            /*HttpRequestResult result = DataManager.Create(dataset);
            if (result != null)
            {
                if (result.Status == Constants.STATUS_SUCCESS)
                {
                    dataset = result.Body as Dataset;
                    if (dataset != null)
                    {
                        MessageBox.Show("Dataset with ID=" + dataset.ID + " has been created successfully.",
                            Constants.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Exversion.Globals.CurrentUser.Datasets.Add(dataset);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(result.Message, Constants.APP_NAME,
                        MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }
            }
            MessageBox.Show("Dataset creation failed!\nDetails:\n" + Exversion.Globals.LastErrorDescription,
                            Constants.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);*/
        }
    }
}
