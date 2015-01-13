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
    public partial class dlgSelectData : Form
    {
        public dlgSelectData()
        {
            InitializeComponent();
            
            lstDatasets.DataSource = Exversion.Globals.CurrentUser.Datasets;
            lstDatasets.DisplayMember = "DisplayName";
            lstDatasets.ValueMember = "ID";
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            DatasetInfo dataset = ApiConsumer.GetMetaData(lstDatasets.SelectedValue.ToString());
            if (dataset != null)
            {
                if (dataset.Columns.Length > 0 && dataset.RowCount > 0)
                {
                    if (new dlgFilterImportData(dataset).ShowDialog() == DialogResult.OK)
                        this.Close();
                }else
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
