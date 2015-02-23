namespace Exversion.ExcelAddIn
{
    partial class ExversionRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public ExversionRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabExversion = this.Factory.CreateRibbonTab();
            this.grpMain = this.Factory.CreateRibbonGroup();
            this.grpRemoteDatasets = this.Factory.CreateRibbonGroup();
            this.grpHelp = this.Factory.CreateRibbonGroup();
            this.btnLogin = this.Factory.CreateRibbonButton();
            this.btnExport = this.Factory.CreateRibbonButton();
            this.btnImportMyData = this.Factory.CreateRibbonButton();
            this.btnSearchImport = this.Factory.CreateRibbonButton();
            this.btnUpdateRemote = this.Factory.CreateRibbonButton();
            this.btnUpdateLocal = this.Factory.CreateRibbonButton();
            this.btnAbout = this.Factory.CreateRibbonButton();
            this.btnHelp = this.Factory.CreateRibbonButton();
            this.tabExversion.SuspendLayout();
            this.grpMain.SuspendLayout();
            this.grpRemoteDatasets.SuspendLayout();
            this.grpHelp.SuspendLayout();
            // 
            // tabExversion
            // 
            this.tabExversion.Groups.Add(this.grpMain);
            this.tabExversion.Groups.Add(this.grpRemoteDatasets);
            this.tabExversion.Groups.Add(this.grpHelp);
            this.tabExversion.Label = "Exversion";
            this.tabExversion.Name = "tabExversion";
            // 
            // grpMain
            // 
            this.grpMain.Items.Add(this.btnLogin);
            this.grpMain.Label = "Main";
            this.grpMain.Name = "grpMain";
            // 
            // grpRemoteDatasets
            // 
            this.grpRemoteDatasets.Items.Add(this.btnExport);
            this.grpRemoteDatasets.Items.Add(this.btnImportMyData);
            this.grpRemoteDatasets.Items.Add(this.btnSearchImport);
            this.grpRemoteDatasets.Items.Add(this.btnUpdateRemote);
            this.grpRemoteDatasets.Items.Add(this.btnUpdateLocal);
            this.grpRemoteDatasets.Label = "Manage Datasets";
            this.grpRemoteDatasets.Name = "grpRemoteDatasets";
            this.grpRemoteDatasets.Visible = false;
            // 
            // grpHelp
            // 
            this.grpHelp.Items.Add(this.btnAbout);
            this.grpHelp.Items.Add(this.btnHelp);
            this.grpHelp.Label = "Info";
            this.grpHelp.Name = "grpHelp";
            // 
            // btnLogin
            // 
            this.btnLogin.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnLogin.Image = global::Exversion.ExcelAddIn.Properties.Resources.login1;
            this.btnLogin.Label = "Login";
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.ShowImage = true;
            this.btnLogin.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLogin_Click);
            // 
            // btnExport
            // 
            this.btnExport.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnExport.Image = global::Exversion.ExcelAddIn.Properties.Resources.new_dataset;
            this.btnExport.Label = "Create New";
            this.btnExport.Name = "btnExport";
            this.btnExport.ShowImage = true;
            this.btnExport.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnExport_Click);
            // 
            // btnImportMyData
            // 
            this.btnImportMyData.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnImportMyData.Image = global::Exversion.ExcelAddIn.Properties.Resources.import_dataset;
            this.btnImportMyData.Label = "Import";
            this.btnImportMyData.Name = "btnImportMyData";
            this.btnImportMyData.ShowImage = true;
            this.btnImportMyData.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnImportMyData_Click);
            // 
            // btnSearchImport
            // 
            this.btnSearchImport.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnSearchImport.Image = global::Exversion.ExcelAddIn.Properties.Resources.search_data;
            this.btnSearchImport.Label = "Search && Import";
            this.btnSearchImport.Name = "btnSearchImport";
            this.btnSearchImport.ShowImage = true;
            this.btnSearchImport.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSearchImport_Click);
            // 
            // btnUpdateRemote
            // 
            this.btnUpdateRemote.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnUpdateRemote.Image = global::Exversion.ExcelAddIn.Properties.Resources.remote_sync;
            this.btnUpdateRemote.Label = "Send Changes";
            this.btnUpdateRemote.Name = "btnUpdateRemote";
            this.btnUpdateRemote.ShowImage = true;
            this.btnUpdateRemote.SuperTip = "Update remote dataset with contents of local dataset.";
            this.btnUpdateRemote.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnUpdateRemote_Click);
            // 
            // btnUpdateLocal
            // 
            this.btnUpdateLocal.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnUpdateLocal.Image = global::Exversion.ExcelAddIn.Properties.Resources.local_sync;
            this.btnUpdateLocal.Label = "Fetch Updates";
            this.btnUpdateLocal.Name = "btnUpdateLocal";
            this.btnUpdateLocal.ShowImage = true;
            this.btnUpdateLocal.SuperTip = "Update local dataset with contents of remote dataset.";
            this.btnUpdateLocal.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnUpdateLocal_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnAbout.Image = global::Exversion.ExcelAddIn.Properties.Resources.about;
            this.btnAbout.Label = "About";
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.ShowImage = true;
            this.btnAbout.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAbout_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnHelp.Image = global::Exversion.ExcelAddIn.Properties.Resources.Help;
            this.btnHelp.Label = "Help";
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.ShowImage = true;
            this.btnHelp.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnHelp_Click);
            // 
            // ExversionRibbon
            // 
            this.Name = "ExversionRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tabExversion);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.ExversionRibbon_Load);
            this.tabExversion.ResumeLayout(false);
            this.tabExversion.PerformLayout();
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.grpRemoteDatasets.ResumeLayout(false);
            this.grpRemoteDatasets.PerformLayout();
            this.grpHelp.ResumeLayout(false);
            this.grpHelp.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabExversion;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpMain;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnLogin;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpRemoteDatasets;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnExport;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnImportMyData;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSearchImport;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpHelp;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAbout;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnHelp;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnUpdateRemote;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnUpdateLocal;
    }

    partial class ThisRibbonCollection
    {
        internal ExversionRibbon ExversionRibbon
        {
            get { return this.GetRibbon<ExversionRibbon>(); }
        }
    }
}
