using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuroImport.Exceptions
{

    [Serializable]
    public class ValueException : Exception
    {
        public string ColumnName { get; set; }
        public string Value { get; set; }
        public string SKU { get; set; }
        public int Line { get; set; }
        public ValueException(string columnName, string value, int line, string sku)
        {
            ColumnName = columnName;
            Value = value;
            Line = line;
            SKU = sku;
        }
        public ValueException(string message) : base(message) { }
    }
}
