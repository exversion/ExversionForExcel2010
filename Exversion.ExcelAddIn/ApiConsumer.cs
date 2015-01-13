using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

using Exversion.API;
using Exversion.Entities;
using Exversion.Utils;

namespace Exversion.ExcelAddIn
{
    static class ApiConsumer
    {
        public static string LastErrorDescription { get; set; }

        public static bool Login(string username, string password, bool displayMsg)
        {
            User user = new User()
            {
                UserName = username,
                Password = password
                //UserName =Security.Decrypt(username),
                //Password = Security.Decrypt(password)
            };

            HttpRequestResult result = Authentication.Login(user);
            if (result != null)
            {
                if (result.Status == Constants.STATUS_SUCCESS)
                {
                    user = result.Body as User;
                    if (user != null)
                    {
                        user.UserName = username;
                        Exversion.Globals.CurrentUser = user;
                        Global.AppSettings.IsUserLoggged = true;
                        Global.OnSessionLogged(true);
                        return true;
                    }
                }
                else
                {
                    if(displayMsg)
                        MessageBox.Show(result.Message, Constants.APP_NAME,
                            MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return false;
                }
            }
            if (displayMsg)
                MessageBox.Show("Login failed!\nDetails:\n" + Exversion.Globals.LastErrorDescription,
                                Constants.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
        }

        public static bool CreateDataset(DatasetInfo dataset)
        {
            HttpRequestResult result = DataManager.Create(dataset);
            if (result != null)
            {
                if (result.Status == Constants.STATUS_SUCCESS)
                {
                    dataset.ID = (result.Body as DatasetInfo).ID;
                    if (dataset != null)
                    {
                        if (Exversion.Globals.CurrentUser.Datasets == null)
                            Exversion.Globals.CurrentUser.Datasets = new List<DatasetInfo>();
                        Exversion.Globals.CurrentUser.Datasets.Add(dataset);
                        return true;
                    }
                }
                else
                    LastErrorDescription = result.Message;
            }
            else
                LastErrorDescription = Exversion.Globals.LastErrorDescription;

            return false;
        }
        public static string ForkDataset(DatasetInfo dataset)
        {
            HttpRequestResult result = DataManager.Fork(dataset);
            if (result != null)
            {
                if (result.Status == Constants.STATUS_SUCCESS)
                {
                    DatasetInfo ds = result.Body as DatasetInfo;
                    if (ds != null)
                    {
                        if (Exversion.Globals.CurrentUser.Datasets == null)
                            Exversion.Globals.CurrentUser.Datasets = new List<DatasetInfo>();
                        Exversion.Globals.CurrentUser.Datasets.Add(ds);
                        return ds.ID;
                    }
                }
                else
                    LastErrorDescription = result.Message;
            }
            else
                LastErrorDescription = Exversion.Globals.LastErrorDescription;

            return null;
        }

        public static bool PushData(DatasetInfo dataset)
        {
            HttpRequestResult result = DataManager.Push(dataset);
            if (result != null)
            {
                if (result.Status == Constants.STATUS_SUCCESS)
                    return true;
                else
                    LastErrorDescription = result.Message;
            }
            else
                LastErrorDescription = Exversion.Globals.LastErrorDescription;

            return false;
        }
        public static PushedDataResults PushData(ExcelDataset dataset)
        {
            //return true;
            HttpRequestResult result = DataManager.PushRows(dataset);
            if (result != null)
            {
                if (result.Status == Constants.STATUS_SUCCESS)
                    return result.Body as PushedDataResults;
                else
                    LastErrorDescription = result.Message;
            }
            else
                LastErrorDescription = Exversion.Globals.LastErrorDescription;

            return null;
        }
        public static bool EditData(ExcelDataset dataset)
        {
            //return true;
            HttpRequestResult result = DataManager.EditRows(dataset);
            if (result != null)
            {
                if (result.Status == Constants.STATUS_SUCCESS)
                    return true;
                else
                    LastErrorDescription = result.Message;
            }
            else
                LastErrorDescription = Exversion.Globals.LastErrorDescription;

            return false;
        }
        public static bool DeleteData(ExcelDataset dataset)
        {
            //return true;
            HttpRequestResult result = DataManager.DeleteRows(dataset);
            if (result != null)
            {
                if (result.Status == Constants.STATUS_SUCCESS)
                    return true;
                else
                    LastErrorDescription = result.Message;
            }
            else
                LastErrorDescription = Exversion.Globals.LastErrorDescription;

            return false;
        }

        public static DatasetInfo GetMetaData(string datasetID)
        {
            HttpRequestResult result = SimpleQuery.GetMetaData(datasetID);
            if (result != null)
            {
                if (result.Status == Constants.STATUS_SUCCESS)
                {
                    return result.Body as DatasetInfo;
                }
                else
                    LastErrorDescription = result.Message;
            }
            else
                LastErrorDescription = Exversion.Globals.LastErrorDescription;

            return null;
        }

        public static dynamic GetRows(string datasetID,string query,bool parseAsArray=false)
        {
            HttpRequestResult result = SimpleQuery.GetRows(datasetID, query, parseAsArray);
            if (result != null)
            {
                if (result.Status == Constants.STATUS_SUCCESS)
                    return result.Body;
                else
                    LastErrorDescription = result.Message;
            }
            else
                LastErrorDescription = Exversion.Globals.LastErrorDescription;

            return null;
        }

        public static List<DatasetInfo> Search(string q, bool _public, ref bool success)
        {
            HttpRequestResult result = SimpleQuery.Search(q, _public);
            if (result != null)
            {
                if (result.Status == Constants.STATUS_SUCCESS)
                {
                    success = true;
                    return result.Body;
                }
                else
                    LastErrorDescription = result.Message;
            }
            else
                LastErrorDescription = Exversion.Globals.LastErrorDescription;

            return null;
        }

        public static bool AlterColumns(string datasetID, List<string> AddedColumns, List<string> RemovedColumns)
        {
            DataManager.Schema(datasetID, AddedColumns, RemovedColumns);

            return true;
        }
    }
}
