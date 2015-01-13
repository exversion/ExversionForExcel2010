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
    public partial class dlgLogin : Form
    {
        
        public dlgLogin()
        {
            InitializeComponent();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtUserName.Text) &&
                !string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                if (ApiConsumer.Login(txtUserName.Text, txtPassword.Text, true))
                {
                    Global.AppSettings.Username = txtUserName.Text;
                    Global.AppSettings.Password = txtPassword.Text;
                    Global.AppSettings.KeepMeSigned = chkKeepMe.Checked;
                    Settings.SaveSettings(Global.AppSettings);
                    this.Close();
                }
            }else
                MessageBox.Show("Enter a username and password please!", 
                    Constants.APP_NAME,MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void lnkForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.exversion.com/resetting/request");
        }

        private void lnkSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://exversion.com/");
        }
       
    }
}
