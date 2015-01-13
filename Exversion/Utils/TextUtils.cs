using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Exversion.Entities;

namespace Exversion.Utils
{
    public static class TextUtils
    {
        public static string GetURL(string controller, string verb)
        {
            return string.Format("{0}{1}",
                Constants.URLs.BASE_URL,
                string.Format(Constants.URLs.API_URL, controller, verb));
        }
        
        public static string GetURL(string controller, string id, string query)
        {
           return string.Format("{0}{1}",
                                     Constants.URLs.BASE_URL,
                                     string.Format(Constants.URLs.API_URL2,
                                     controller, id,Globals.CurrentUser.ApiKey+query));
        }
        public static string GetSearchURL(string q, string _public)
        {
            return string.Format("{0}{1}",
                                      Constants.URLs.BASE_URL,
                                      string.Format(Constants.URLs.API_URL3,
                                                    string.Format(Constants.Controller.SEARCH,q,_public)));
        }
        public static string GetLoginJson(User user)
        {
            return string.Format(Constants.RequestJSonBody.LOGIN,
                                    user.UserName, user.Password);
        }
        
        public static string GetCreateDatasetJson(DatasetInfo dataset)
        {
            return string.Format(Constants.RequestJSonBody.DATASET_CREATE,
                Globals.CurrentUser.AccessToken,dataset.Name,dataset.Description,
                dataset.SourceUrl,dataset.Organization,dataset.SourceAuthor
                ,dataset.SourceDate,dataset.SourceContact,dataset.Private);
        }

        public static string GetForkDatasetJson(DatasetInfo dataset)
        {
            return string.Format(Constants.RequestJSonBody.DATASET_FORK,
                Globals.CurrentUser.AccessToken, dataset.Name, dataset.ID);
        }
        public static string GetPushDatasetJson(DatasetInfo dataset)
        {
            return string.Format(Constants.RequestJSonBody.DATASET_PUSH,
                Globals.CurrentUser.AccessToken, dataset.ID,dataset.JsonData);
        }

        public static string GetPushDatasetJson(ExcelDataset dataset)
        {
            return string.Format(Constants.RequestJSonBody.DATASET_PUSH,
                Globals.CurrentUser.AccessToken, dataset.ID, GetPushDatapointsJson(dataset));
        }
        private static string GetPushDatapointsJson(ExcelDataset dataset)
        {
            int colCount = dataset.SelectedColumns.Count;
            string datapoint, datapoints, pair;
            datapoints = string.Empty;
            string colName;

            foreach (Row row in dataset.Rows)
            {
                datapoint = "{";
                for (int i = 0; i < colCount; i++)
                {
                    colName=dataset.SelectedColumns[i];
                    pair = string.Format(Constants.RequestJSonBody.DATASET_DATAPOINT_PAIR,
                                            colName, row.Cells[colName]);
                    if (i > 0)
                        pair = "," + pair;
                    datapoint += pair;
                }
                datapoint += "}";
                if (!string.IsNullOrEmpty(datapoints))
                    datapoints += ",";

                datapoints += datapoint;
            }
            
            return string.Format(Constants.RequestJSonBody.DATASET_DATAPOINTS_PUSH, datapoints);;
        }
        public static string GetEditDatasetJson(ExcelDataset dataset)
        {
            return string.Format(Constants.RequestJSonBody.DATASET_EDIT,
                Globals.CurrentUser.AccessToken,GetEditDatapointsJson(dataset));
        }
        
        private static string GetEditDatapointsJson(ExcelDataset dataset)
        {
            int colCount = dataset.SelectedColumns.Count;
            string change, datapoints, pair;
            datapoints = string.Empty;
            string colName;

            foreach (Row row in dataset.Rows)
            {
                change = string.Empty;
                for (int i = 0; i < colCount; i++)
                {
                    colName = dataset.SelectedColumns[i];
                    pair = string.Format(Constants.RequestJSonBody.DATASET_DATAPOINT_PAIR,
                                            colName, row.Cells[colName]);
                    if (i > 0)
                        pair = "," + pair;
                    change += pair;
                }
                if (!string.IsNullOrEmpty(datapoints))
                    datapoints += ",";

                datapoints += string.Format(Constants.RequestJSonBody.DATASET_DATAPOINTS_EDIT,
                                            dataset.ID, row.ID, change);
            }

            return datapoints;
        }

        public static string GetDeleteDatasetJson(ExcelDataset dataset)
        {
            return string.Format(Constants.RequestJSonBody.DATASET_DELETE,
                Globals.CurrentUser.AccessToken, GetDeleteDatapointsJson(dataset));
        }

        private static string GetDeleteDatapointsJson(ExcelDataset dataset)
        {
            string datapoint, datapoints;
            datapoints = string.Empty;

            foreach (Row row in dataset.Rows)
            {
                datapoint = string.Format(Constants.RequestJSonBody.DATASET_DATAPOINTS_DELETE,
                                            dataset.ID, row.ID);
                
                if (!string.IsNullOrEmpty(datapoints))
                    datapoints += ",";

                datapoints += datapoint;
            }

            return datapoints;
        }

        public static string GetSchemaDatasetJson(string datasetID, List<string> AddedColumns, List<string> RemovedColumns)
        {
            string columns = string.Empty;
            if (AddedColumns.Count > 0)
                columns = GetSchemaColumnsJson(datasetID, AddedColumns,Constants.Strings.ADD);
            if(RemovedColumns.Count>0)
                columns += "," + GetSchemaColumnsJson(datasetID, RemovedColumns, Constants.Strings.DELETE);

            return string.Format(Constants.RequestJSonBody.DATASET_SCHEMA,
                                 Globals.CurrentUser.AccessToken, columns);
        }

        private static string GetSchemaColumnsJson(string datasetID, List<string> Columns,string type)
        {
            string column, columns;
            columns = string.Empty;

            foreach (string col in Columns)
            {
                column = string.Format(Constants.RequestJSonBody.DATASET_COLUMNS_SCHEMA,
                                       datasetID, type, col);

                if (!string.IsNullOrEmpty(columns))
                    columns += ",";
                columns += column;
            }

            return columns;
        }

        
    }
}
