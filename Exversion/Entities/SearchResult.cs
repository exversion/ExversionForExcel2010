using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace Exversion.Entities
{
    public class SearchResult
    {
        //[JsonProperty("dataset")]
        public DatasetInfo[] Items { get; set; }
    }
}
