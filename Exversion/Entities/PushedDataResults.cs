using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace Exversion.Entities
{
    public class PushedDataResults
    {
        [JsonProperty("inserted")]
        public dynamic[] InsertedRows { get; set; }
        [JsonProperty("updated")]
        public dynamic[] UpdatedRows { get; set; }
        [JsonProperty("failed")]
        public dynamic[] FailedRows { get; set; }
    }

    public class RowInf
    {
        [JsonProperty("_id")]
        public string ID { get; set; }
        [JsonProperty("_hash")]
        public string Hash { get; set; }
    }
}
