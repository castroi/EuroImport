using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuroImport.Exceptions
{

    [Serializable]
    public class ValueNumericException : ValueException
    {
        public ValueNumericException(string columnName, string value, int line, string sku) 
            : base(columnName, value, line, sku)
        {
        }
    }
}
