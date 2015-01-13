using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Exversion.Entities;
using Exversion.Utils;
using Exversion.Logging;

namespace Exversion.API
{
    public static class DataManager
    {
        public static HttpRequestResult Create(DatasetInfo dataset)
        {
            if (HttpRequestEngine.SendPOSTRequest(TextUtils.GetURL(Constants.Controller.DATASET, 
                                                  Constants.RequestVerb.DATASET_CREATE),
                                                  TextUtils.GetCreateDatasetJson(dataset)))
            {
                return JSonProcessor.ResultFromJson<DatasetInfo>(HttpRequestEngine.Response);
            }
            else
            {
                Globals.LastErrorDescription = HttpRequestEngine.Response;
                Logger.LogEvent("Creating remote dataset", HttpRequestEngine.Response);
                return null;
            }
        }

        public static HttpRequestResult Fork(DatasetInfo dataset)
        {
            if (HttpRequestEngine.SendPOSTRequest(TextUtils.GetURL(Constants.Controller.DATASET,
                                                  Constants.RequestVerb.DATASET_FORK),
                                                  TextUtils.GetForkDatasetJson(dataset)))
            {
                return JSonProcessor.ResultFromJson<DatasetInfo>(HttpRequestEngine.Response);
            }
            else
            {
                Globals.LastErrorDescription = HttpRequestEngine.Response;
                Logger.LogEvent("Forking remote dataset", HttpRequestEngine.Response);
                return null;
            }
        }
        public static HttpRequestResult Push(DatasetInfo dataset)
        {
            if (HttpRequestEngine.SendPOSTRequest(TextUtils.GetURL(Constants.Controller.DATASET,
                                                  Constants.RequestVerb.DATASET_PUSH),
                                                  TextUtils.GetPushDatasetJson(dataset)))
            {
                return JSonProcessor.ResultFromJson<DatasetInfo>(HttpRequestEngine.Response,false);
            }
            else
            {
                Globals.LastErrorDescription = HttpRequestEngine.Response;
                Logger.LogEvent("pushing data to remote dataset", HttpRequestEngine.Response);
                return null;
            }
        }

        public static HttpRequestResult PushRows(ExcelDataset dataset)
        {
            //string r = TextUtils.GetPushDatasetJson(dataset);
            //return null;

            if (HttpRequestEngine.SendPOSTRequest(TextUtils.GetURL(Constants.Controller.DATASET,
                                                  Constants.RequestVerb.DATASET_PUSH),
                                                  TextUtils.GetPushDatasetJson(dataset)))
            {
                return JSonProcessor.ResultFromJson<PushedDataResults>(HttpRequestEngine.Response,toArray:true);
            }
            else
            {
                Globals.LastErrorDescription = HttpRequestEngine.Response;
                Logger.LogEvent("pushing new rows to remote dataset", HttpRequestEngine.Response);
                return null;
            }
        }

        public static HttpRequestResult EditRows(ExcelDataset dataset)
        {
            //string json = TextUtils.GetEditDatasetJson(dataset);
            //return null;

            if (HttpRequestEngine.SendPOSTRequest(TextUtils.GetURL(Constants.Controller.DATASET,
                                                  Constants.RequestVerb.DATASET_EDIT),
                                                  TextUtils.GetEditDatasetJson(dataset)))
            {
                return JSonProcessor.ResultFromJson<DatasetInfo>(HttpRequestEngine.Response, false);
            }
            else
            {
                Globals.LastErrorDescription = HttpRequestEngine.Response;
                Logger.LogEvent("updating rows on remote dataset", HttpRequestEngine.Response);
                return null;
            }
        }

        public static HttpRequestResult DeleteRows(ExcelDataset dataset)
        {
            //string json = TextUtils.GetDeleteDatasetJson(dataset);
            //return null;

            if (HttpRequestEngine.SendPOSTRequest(TextUtils.GetURL(Constants.Controller.DATASET,
                                                  Constants.RequestVerb.DATASET_DELETE),
                                                  TextUtils.GetDeleteDatasetJson(dataset)))
            {
                return JSonProcessor.ResultFromJson<DatasetInfo>(HttpRequestEngine.Response, false);
            }
            else
            {
                Globals.LastErrorDescription = HttpRequestEngine.Response;
                Logger.LogEvent("deleting rows from remote dataset", HttpRequestEngine.Response);
                return null;
            }
        }

        public static HttpRequestResult Schema(string datasetID, List<string> AddedColumns, List<string> RemovedColumns)
        {
            string json = TextUtils.GetSchemaDatasetJson(datasetID, AddedColumns, RemovedColumns);
            //return null;

            if (HttpRequestEngine.SendPOSTRequest(TextUtils.GetURL(Constants.Controller.DATASET,
                                                  Constants.RequestVerb.DATASET_SCHEMA),
                                                  TextUtils.GetSchemaDatasetJson(datasetID, AddedColumns, RemovedColumns)))
            {
                return JSonProcessor.ResultFromJson<DatasetInfo>(HttpRequestEngine.Response, false);
            }
            else
            {
                Globals.LastErrorDescription = HttpRequestEngine.Response;
                Logger.LogEvent("altering schema of remote dataset", HttpRequestEngine.Response);
                return null;
            }
        }
        
    }
}
