using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Exversion.Entities;

namespace Exversion
{
    public static class Globals
    {
        public const string API_KEY = "dfc9a73818";
        public static User CurrentUser { get; set; }
        public static string LastErrorDescription { get; set; }

        
    }
}
