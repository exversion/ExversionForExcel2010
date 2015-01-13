using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exversion
{
    public static class Constants
    {
        public static class Strings
        {
            public static string ID { get { return "_id"; } }
            public static string HASH { get { return "_hash"; } }
            public static string ADD { get { return "insert"; } }//add
            public static string DELETE { get { return "delete"; } }
        }

        public static class URLs
        {
            public static string BASE_URL { get { return "https://exversion.com/"; } }
            public static string API_URL  { get { return  "api/v1/{0}/{1}/";} }
            public static string API_URL2 { get { return "api/v1/{0}/{1}?key={2}"; } }
            public static string API_URL3 { get { return "api/v1/{0}"; } }
            //public static string API_AUTH_URL { get { return  } } "api/v1/excel/{0}/";
            //public static string API_DATASET_URL { get { return  } } "api/v1/excel/{0}/";
        }

        public static class Controller
        {
            public static string EXCEL { get { return "excel"; } }//"excel/{0}/"
            public static string DATASET { get { return "dataset"; } } //"dataset/{0}/"
            public static string METADATA { get { return "metadata"; } } //"dataset/{0}/"
            public static string SEARCH { get { return "search?q={0}&_public={1}"; } } //"dataset/{0}/"
        }

        public static class HTTPMethod
        {
            public static string GET { get { return "GET";} }
            public static string POST { get { return "POST";} } 
        }

        public static class RequestVerb
        {
            public static string REGISTER { get { return "register"; } } 
            public static string LOGIN { get { return  "login"; } }
            public static string DATASET_CREATE { get { return "create"; } }
            public static string DATASET_FORK { get { return "fork"; } }
            public static string DATASET_PUSH { get { return "push"; } } 
            public static string DATASET_EDIT { get { return "edit"; } } 
            public static string DATASET_DELETE { get { return  "delete";} } 
            public static string DATASET_SCHEMA { get { return  "schema"; } }
        }

        public static class RequestJSonBody
        {
            //public static string REGISTER { get { return  "{{\"username\":{0},\"password\":{1}, \"confirm_password\":{2}, \"email\":{3}}}"; } }
            public static string LOGIN { get { return "{{ \"username\":\"{0}\",\"password\":\"{1}\" }}"; } }
            public static string DATASET_CREATE { get { return "{{ \"access_token\":\"{0}\", \"name\":\"{1}\", \"description\":\"{2}\",\"source_url\":\"{3}\", \"org\":{4},\"source_author\":\"{5}\",\"source_date\":\"{6}\", \"source_contact\":\"{7}\", \"private\":{8}}}"; } }
            public static string DATASET_FORK { get { return "{{ \"access_token\":\"{0}\", \"name\":\"{1}\", \"parent\":\"{2}\",\"forkchanges\":\"forked\",\"track\":1, \"private\":0,\"clear\":0 }}"; } }
            public static string DATASET_PUSH { get { return "{{ \"access_token\":\"{0}\", \"dataset\":\"{1}\", \"data\":{2} }}"; } }
            public static string DATASET_EDIT { get { return "{{ \"access_token\":\"{0}\", \"edits\":[{1}] }}"; } }
            public static string DATASET_DELETE { get { return "{{ \"access_token\":\"{0}\", \"deletes\":[{1}] }}"; } }
            public static string DATASET_SCHEMA { get { return "{{ \"access_token\":\"{0}\", \"changes\":[{1}] }}"; } }
            public static string DATASET_DATAPOINT_PAIR { get { return "\"{0}\":\"{1}\""; } }
            public static string DATASET_DATAPOINTS_PUSH { get { return "[{0}]"; } }
            public static string DATASET_DATAPOINTS_EDIT { get { return "{{ \"dataset\":\"{0}\", \"_id\":\"{1}\", \"changes\":{{ {2} }} }}"; } }
            public static string DATASET_DATAPOINTS_DELETE { get { return "{{ \"dataset\":\"{0}\", \"_id\":\"{1}\" }}"; } }
            public static string DATASET_COLUMNS_SCHEMA { get { return "{{ \"dataset\":\"{0}\", \"type\":\"{1}\", \"column_name\":\"{2}\" }}"; } }
            
        }
    }
}
