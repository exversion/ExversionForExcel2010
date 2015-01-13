using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using Exversion.Utils;
using Exversion.Logging;

namespace Exversion.ExcelAddIn
{
    public class Settings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool KeepMeSigned { get; set; }
        public bool IsUserLoggged { get; set; }

        public static void SaveSettings(Settings settings)
        {
            string user = settings.Username;
            string password = settings.Password;

            settings.Username = Security.Encrypt(settings.Username);
            settings.Password = Security.Encrypt(settings.Password);

            try
            {
                if (!File.Exists(Global.SettingsPath))
                    File.Create(Global.SettingsPath).Close();

                using (var writer = new System.IO.StreamWriter(Global.SettingsPath))
                {
                    var serializer = new XmlSerializer(settings.GetType());
                    serializer.Serialize(writer, settings);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                Logger.LogEvent("Saving settings", ex.Message);
            }

            settings.Username = user;
            settings.Password = password;
        }
        public static Settings LoadSettings()
        {
            try
            {
                using (var stream = System.IO.File.OpenRead(Global.SettingsPath))
                {
                    var serializer = new XmlSerializer(typeof(Settings));
                    Settings settings = serializer.Deserialize(stream) as Settings;
                    settings.Username = Security.Decrypt(settings.Username);
                    settings.Password = Security.Decrypt(settings.Password);

                    return settings;
                }
            }
            catch (Exception ex)
            {
                Logger.LogEvent("Loading settings", ex.Message);
                return new Settings();
            }
        }
    }
}
