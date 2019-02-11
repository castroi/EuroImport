using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuroImport.Exceptions
{
    [Serializable]
    public class SlugsException : ValueException
    {
        public SlugsException(string columnName, string value, int line, string sku)
            : base(columnName, value, line, sku)
        { }
    }
}
