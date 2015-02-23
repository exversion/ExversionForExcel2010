using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using System.IO;
using System.Diagnostics;
using Exversion.Logging;

namespace Exversion.ExcelAddIn
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            System.Threading.ThreadPool.QueueUserWorkItem((action) => init());
        }

        private void init()
        {
            try
            {
                Global.AppPath = Path.Combine(
                   Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                   "Exversion For Excel");

                Logger.LogPath = Path.Combine(Global.AppPath, "events.log");
                Global.SettingsPath = Path.Combine(Global.AppPath, "settings.config");

                Global.AppSettings = Settings.LoadSettings();
                if (Global.AppSettings.KeepMeSigned &&
                    Global.AppSettings.IsUserLoggged &&
                    !string.IsNullOrWhiteSpace(Global.AppSettings.Username) &&
                    !string.IsNullOrWhiteSpace(Global.AppSettings.Password))
                {
                    if (!ApiConsumer.Login(Global.AppSettings.Username, Global.AppSettings.Password, false))
                    {
                        Global.AppSettings.IsUserLoggged = false;
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.LogEvent("Initializing app", ex.Message);
            }

            ExcelUtils.ExcelApp = this.Application;
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
