using Exversion.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exversion.ExcelAddIn
{
    public static class RowComparers
    {
        public class IDComparer : IEqualityComparer<Row>
        {
            #region IEqualityComparer<Row> Members

            public bool Equals(Row x, Row y)
            {
                return x.ID == y.ID;
            }

            public int GetHashCode(Row row)
            {
                return (row.ID == null) ? 0 : row.ID.GetHashCode();
            }

            #endregion
        }

        public class HashComparer : IEqualityComparer<Row>
        {
            #region IEqualityComparer<Row> Members

            public bool Equals(Row x, Row y)
            {
                return x.Hash == y.Hash;
            }

            public int GetHashCode(Row row)
            {
                return row.Hash.GetHashCode();
            }

            #endregion
        }

        public class RowComparer : IEqualityComparer<Row>
        {
            #region IEqualityComparer<Row> Members

            public bool Equals(Row x, Row y)
            {
                //(x.ID == y.ID) && (x.Hash == y.Hash);
                return !((x.ID == y.ID) && (x.Hash != y.Hash));
            }

            public int GetHashCode(Row row)
            {
                return (row.ID + row.Hash).GetHashCode();
            }

            #endregion
        }
    }
}
