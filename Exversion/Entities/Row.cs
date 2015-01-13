using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Exversion.Entities
{
    public class Row
    {
        [XmlAttribute]
        public string ID { get; set; }
        [XmlAttribute]
        public string Hash { get; set; }

        [XmlIgnoreAttribute]
        public int IndexInRange { get; set; }
        [XmlIgnoreAttribute]
        public string OldHash { get; set; }
        [XmlIgnoreAttribute]
        public string Text { get; set; }
        [XmlIgnoreAttribute]
        //public List<string> Cells { get; set; }
        public Dictionary<string, dynamic> Cells { get; set; }

        

        public Row()
        {
            //Cells = new List<string>();
            Cells = new Dictionary<string, object>();
        }

        public override string ToString()
        {
            return Utils.Converter.DictionaryToString(Cells);
        }

        public string GetText()
        {
            return Utils.Converter.DictionaryToString(Cells,true);
        }
    }
}
