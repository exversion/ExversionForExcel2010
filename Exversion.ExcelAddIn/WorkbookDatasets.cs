using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Exversion.Entities;

namespace Exversion.ExcelAddIn
{
    public class WorkbookDatasets
    {
        public string ID { get; set; }
        public List<ExcelDataset> Datasets { get; set; }
        //public List<WorksheetDataset> Datasets { get; set; }
    }

    public class WorksheetDataset
    {
        public string RangeName { get; set; }
        public ExcelDataset Dataset { get; set; }
    }
}
