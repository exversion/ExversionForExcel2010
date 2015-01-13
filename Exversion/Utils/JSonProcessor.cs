using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Exversion.Entities;
using Exversion.Logging;

namespace Exversion.Utils
{
    public class JSonProcessor
    {
        public static HttpRequestResult ResultFromJson<TBODY>(string json, bool parseBody = true, bool castToTBODY=true,bool toArray=false) where TBODY : class
        {
            HttpRequestResult result = null;
 
            try
            {
                result = JsonConvert.DeserializeObject<HttpRequestResult>(json);
                if (parseBody)
                {
                    result.Body = JsonConvert.DeserializeObject(result.Body.ToString());
                    if (castToTBODY)
                    {
                        if(toArray)
                            result.Body = JsonConvert.DeserializeObject<TBODY>(result.Body.ToString());
                        else
                            result.Body = JsonConvert.DeserializeObject<TBODY>(result.Body[0].ToString());
                    }
                    else
                        result.Body = JsonConvert.DeserializeObject(result.Body.ToString());
                }
            }
            catch (Exception ex)
            {
                Globals.LastErrorDescription = ex.Message;
                Logger.LogEvent("Deserializing JSON string", ex.Message);
                if (result != null)
                    result.Body = null;
            }
            
            return result;
        }

        public static string FormatJSon(string json)
        {
            return JToken.Parse(json).ToString();
        }
    }
}
