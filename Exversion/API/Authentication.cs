using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Exversion.Entities;
using Exversion.Utils;
using Exversion.Logging;

namespace Exversion.API
{
    public static class Authentication
    {
        public static HttpRequestResult Login(User user)
        {
            if (HttpRequestEngine.SendPOSTRequest(TextUtils.GetURL(Constants.Controller.EXCEL, Constants.RequestVerb.LOGIN),
                                                 TextUtils.GetLoginJson(user)))
            {
                return JSonProcessor.ResultFromJson<User>(HttpRequestEngine.Response);
            }
            else
            {
                Globals.LastErrorDescription = HttpRequestEngine.Response;
                Logger.LogEvent("login to exversion.com", HttpRequestEngine.Response);
                return null;
            }
        }
    }
}
