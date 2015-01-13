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
    public partial class dlgSearchData : Form
    {
        List<DatasetInfo> results;

        public dlgSearchData()
        {
            InitializeComponent();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            grdResults.Rows.Clear();
            bool success = false;
            results = ApiConsumer.Search(txtQ.Text, chkPublic.Checked,ref success);

            if (success)
            {
                if (results != null && results.Count > 0)
                {
                    foreach (DatasetInfo dataset in results)
                    {
                        int index = grdResults.Rows.Add(dataset.ID, dataset.Name, dataset.Description);
                        grdResults.Rows[index].HeaderCell.Value = (index + 1).ToString();
                    }
                    grdResults.Rows[0].Selected = true;
                    cmdOK.Enabled = true;
                    return;
                }
                else
                {
                    MessageBox.Show("No Data was found!", Constants.APP_NAME,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Could not run the query!\nDetails:\n" +
               ApiConsumer.LastErrorDescription, Constants.APP_NAME,
               MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            cmdOK.Enabled = false;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            DataGridViewRow r = grdResults.SelectedRows[0];
            int index = r.Index;
            string id = results[index].ID;
           
            DatasetInfo dataset = ApiConsumer.GetMetaData(id);
            if (dataset != null)
            {
                if (dataset.Columns.Length > 0 && dataset.RowCount > 0)
                {
                    if (new dlgFilterImportData(dataset).ShowDialog() == DialogResult.OK)
                        this.Close();
                }
                else
                {
                    MessageBox.Show("The selected Dataset contains no values!", Constants.APP_NAME,
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(ApiConsumer.LastErrorDescription, Constants.APP_NAME,
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
