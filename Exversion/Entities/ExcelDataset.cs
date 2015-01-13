using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Exversion.Entities
{
    public class ExcelDataset
    {
        [XmlAttribute]
        public string ID { get; set; }
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Hash { get; set; }
        [XmlAttribute]
        public int RowCount { get; set; }
        [XmlAttribute]
        public int QueryRowCount { get; set; }
        [XmlAttribute]
        public int MaxRows { get; set; }
        [XmlAttribute]
        public string Query { get; set; }
        [XmlAttribute]
        public string RangeName { get; set; }
        [XmlAttribute]
        public DateTime CreationDate { get; set; }
        [XmlAttribute]
        public DateTime LastUpdate { get; set; }

        public List<string> SelectedColumns { get; set; }
        public List<string> ExcludedColumns { get; set; }
//        public List<string> RemovedRows { get; set; }
        public List<Row> Rows { get; set; }

        [XmlIgnoreAttribute]
        public string Text { get; set; }
        [XmlIgnore]
        public string UpdatedHash { get; set; }
        [XmlIgnore]
        public object AssociatedWorksheet { get; set; }
        [XmlIgnore]
        public string FullQuery
        {
            get
            {
                string query=Query;
                if (MaxRows == 0)
                {
                    if(string.IsNullOrEmpty(Query))
                        query = "&_limit=" + RowCount;
                }
                else
                {
                    query = Query + "&_limit=" + MaxRows;
                }
                return query;
            }
        }

        public ExcelDataset()
        {
            Rows = new List<Row>();
            SelectedColumns = new List<string>();
            ExcludedColumns = new List<string>();
        }
        public override string ToString()
        {
            //string result = Rows[0].ToString() + Environment.NewLine;
            //for (int i = 1; i < Rows.Count; i++)
            //    result += Rows[i].ToString() + Environment.NewLine;
            string result = string.Empty;
            for (int i = 0; i < Rows.Count; i++)
                result += Rows[i].Text + Environment.NewLine;
            
           
                //Hash = Utils.Hash.SHA1(result);
            return result;
        }
    }
}
