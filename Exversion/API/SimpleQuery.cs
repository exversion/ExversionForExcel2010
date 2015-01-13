using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Exversion.Entities;
using Exversion.Utils;
using System.Diagnostics;
using Exversion.Logging;

namespace Exversion.API
{
    public static class SimpleQuery
    {
        public static HttpRequestResult GetMetaData(string datasetID)
        {
            if (HttpRequestEngine.SendGETRequest(TextUtils.GetURL(Constants.Controller.METADATA, datasetID, string.Empty)))
            {
                return JSonProcessor.ResultFromJson<DatasetInfo>(HttpRequestEngine.Response);
            }
            else
            {
                Globals.LastErrorDescription = HttpRequestEngine.Response;
                Logger.LogEvent("getting metadata of remote dataset", HttpRequestEngine.Response);
                return null;
            }
        }

        public static HttpRequestResult GetRows(string datasetID, string query,bool parseAsArray)
        {
            if (HttpRequestEngine.SendGETRequest(TextUtils.GetURL(Constants.Controller.DATASET, datasetID, query)))
            {
                //return JSonProcessor.ResultFromJson<List<Dictionary<string, string>>>(HttpRequestEngine.Response, toArray: true);
                return JSonProcessor.ResultFromJson<List<Dictionary<string, dynamic>>>(HttpRequestEngine.Response, toArray: true);
            }
            else
            {
                Globals.LastErrorDescription = HttpRequestEngine.Response;
                Logger.LogEvent("getting rows of remote dataset", HttpRequestEngine.Response);
                return null;
            }
        }

        public static HttpRequestResult Search(string q, bool onlyPublic)
        {
            //string result = "{\"status\":200, \"message\":\"Success\", \"body\":[ 	{\"dataset\":\"L82OG9Q477GUNHZ\",\"name\":\"Orders 1.1\"}, 	{\"dataset\":\"OVM8UNRN9U4S9GXM\",\"name\":\"WATER DEPTH\"}, 	{\"dataset\":\"DFK26VXU46QXDAGO\",\"name\":\"2009 Preliminary Toxics Release\"}, 	{\"dataset\":\"2XCZNB2SO13ESSZ8\",\"name\":\"BRUTSAERT (FIFE)\"}, 	{\"dataset\":\"Q73RKQUIMG6TQUY2\",\"name\":\"1988 Toxics Release Inventory\"}, 	{\"dataset\":\"XM79JRHWZ90S8ZV2\",\"name\":\"OCEAN BEACH PIER, CA SURGE\"}, 	{\"dataset\":\"88U7OLZZL3ZQAKWA\",\"name\":\"Street Tree Census\"} 	] 	}";
            //return JSonProcessor.ResultFromJson<List<Dataset>>(result,toArray:true);
            string url = TextUtils.GetSearchURL(q, Convert.ToInt32(onlyPublic).ToString());
            if (HttpRequestEngine.SendGETRequest(url))
            {
                return JSonProcessor.ResultFromJson<List<DatasetInfo>>(HttpRequestEngine.Response, toArray: true);
            }
            else
            {
                Globals.LastErrorDescription = HttpRequestEngine.Response;
                Logger.LogEvent("searching for remote datasets", HttpRequestEngine.Response);
                return null;
            }
        }
    }
}
