using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace Exversion.Entities
{
    public class User
    {
        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("token")]
        public string AccessToken { get; set; }
        [JsonProperty("key")]
        public string ApiKey { get; set; }
        [JsonProperty("private")]
        public string Private { get; set; }
        [JsonProperty("datasets")]
        public List<DatasetInfo> Datasets { get; set; }
        //public Dataset[] Datasets { get; set; }
    }
}
