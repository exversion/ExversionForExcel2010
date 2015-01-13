using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Exversion.Logging;

namespace Exversion.Utils
{
    internal static class HttpRequestEngine
    {
        private static HttpWebRequest webRequest;

        public static string Response { get; set; }
        public static int StatusCode { get; set; }

        public static bool SendPOSTRequest(string url, string postData)
        {
            webRequest = CreateWebRequest(url, Constants.HTTPMethod.POST);

            if (webRequest == null)
                return false;

            StreamWriter requestWriter;
            HttpWebResponse resp;

            try
            {
                using (requestWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    requestWriter.Write(postData);
                }

                resp = (HttpWebResponse)webRequest.GetResponse();
                StatusCode = (int)resp.StatusCode;
                Stream resStream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                Response = JSonProcessor.FormatJSon(reader.ReadToEnd());
                return true;
            }
            catch (Exception ex)
            {
                Response = ex.Message;
                //Logger.LogEvent("Sending POST request", ex.Message);
                return false;
            }
        }

        public static bool SendGETRequest(string url)
        {
            webRequest = CreateWebRequest(url, Constants.HTTPMethod.GET);
            try
            {
                using (WebResponse respo = webRequest.GetResponse())
                {
                    StatusCode = (int)(((HttpWebResponse)respo).StatusCode);
                    using (Stream stream = respo.GetResponseStream())
                    {
                        Response = JSonProcessor.FormatJSon(new StreamReader(stream).ReadToEnd());
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Response = ex.Message;
                //Logger.LogEvent("Sending GET request", ex.Message);
                return false;
            }
        }

        private static HttpWebRequest CreateWebRequest(string url, string Method)
        {
            HttpWebRequest myWebRequest = WebRequest.Create(url) as HttpWebRequest;

            if (myWebRequest != null)
            {
                myWebRequest.Method = Method;
                myWebRequest.ServicePoint.Expect100Continue = false;
                myWebRequest.Timeout = 20 * 1000;
                myWebRequest.ContentType = "application/json";
                myWebRequest.KeepAlive = false;
            }

            return myWebRequest;
        }
    }
}
