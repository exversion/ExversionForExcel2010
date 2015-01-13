using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Exversion.Logging
{
    public static class Logger
    {
        public static string LogPath;
        public static void LogEvent(string title,string description)
        {
            File.AppendAllText(LogPath, string.Format("{0};{1}: {2}\n",
                DateTime.Now.ToString(), title, description));
        }
    }
}
