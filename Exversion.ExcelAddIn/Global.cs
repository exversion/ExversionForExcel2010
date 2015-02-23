using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Exversion.Entities;

namespace Exversion.ExcelAddIn
{
    static class Global
    {
        public static bool IsUserLoggged { get; set; }
        public static string AppPath { get; set; }
        public static string SettingsPath { get; set; }
        public static Settings AppSettings { get; set; }

        public static int StartingRowIndex { get; set; }
        public static int StartingColumnIndex { get; set; }
        
        public static string ProgressInfo { get; set; }
        public static bool ProgressIsFinished { get; set; }

        public static string PreviewJSON { get; set; }

        public static void OnSessionLogged(bool logged)
        {
            ExversionRibbon ribbon= Globals.Ribbons.ExversionRibbon;
            if (logged)
            {
                ribbon.btnLogin.Label = "Logout";
                ribbon.btnLogin.Image = global::Exversion.ExcelAddIn.Properties.Resources.logout;
            }
            else
            {
                ribbon.btnLogin.Label = "Login";
                ribbon.btnLogin.Image = global::Exversion.ExcelAddIn.Properties.Resources.login1;
            }

            ribbon.grpRemoteDatasets.Visible = logged;
        }
    }
}
