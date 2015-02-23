using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Exversion.ExcelAddIn.Dialogs
{
    public partial class dlgBusy : Form
    {
        public dlgBusy()
        {
            InitializeComponent();
            lblTitle.Text = Global.ProgressInfo;
        }
      
        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            //lblTitle.Text = Global.ProgressInfo;
            if (Global.ProgressIsFinished)
            {
                this.Close();
            }
            else if (!Global.ProgressInfo.Equals(lblTitle.Text))
            {
                lblTitle.Text = Global.ProgressInfo;
            }
        }
    }
}
