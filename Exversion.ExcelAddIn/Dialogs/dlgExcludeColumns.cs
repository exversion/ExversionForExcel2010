using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Exversion.ExcelAddIn.Dialogs
{
    public partial class dlgExcludeColumns : Form
    {
        private Dictionary<string, bool> columns;
        public Dictionary<string, bool> Columns 
        {
            get
            {
                return columns;
            }
            set
            {
                columns = value;
                lstColumns.Items.Clear();
                foreach (KeyValuePair<string,bool> col in columns)
                {
                    lstColumns.Items.Add(col.Key);
                }

                for (int i = 0; i < lstColumns.Items.Count; i++)
                    lstColumns.SetItemChecked(i, columns[lstColumns.Items[i].ToString()]);
            }
        }
        public dlgExcludeColumns()
        {
            InitializeComponent();
        }

        private void lstColumns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string key=lstColumns.Items[e.Index].ToString();
            bool _checked=(e.NewValue==CheckState.Checked);
            columns[key] = _checked;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.OK;
        }
       
    }
}
