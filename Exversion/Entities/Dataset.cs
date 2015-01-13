using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace Exversion.Entities
{
    public class DatasetInfo
    {
        [JsonProperty("dataset")]
        public string ID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("private")]
        public int Private { get; set; }
        //[JsonProperty("data")]
        public string DisplayName { get { return Name + " (" + ID + ")"; } }
        public string JsonData { get; set; }
        [JsonProperty("source_url")]
        public string SourceUrl { get; set; }
        //[JsonProperty("forkchanges")]
        //public string ForkChanges { get; set; }
        [JsonProperty("size")]
        public string Size { get; set; }
        //[JsonProperty("heritage")]
        //public string Heritage { get; set; }
        //[JsonProperty("parent")]
        //public string Parent { get; set; }
        //[JsonProperty("clear")]
        //public string Clear { get; set; }
        [JsonProperty("columns")]
        public string[] Columns { get; set; }
        [JsonProperty("rows")]
        public int RowCount { get; set; }
        //[JsonProperty("uploadedBy")]
        //public dynamic UploadedBy { get; set; }
        [JsonProperty("org")]
        public int Organization { get; set; }
        [JsonProperty("source_author")]
        public string SourceAuthor { get; set; }
        [JsonProperty("source_date")]
        public string SourceDate { get; set; }
        [JsonProperty("source_contact")]
        public string SourceContact { get; set; }
     }
}
