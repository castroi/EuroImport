using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuroImport.Exceptions
{

    [Serializable]
    public class ColumnMissingException : Exception
    {
        public ColumnMissingException() { }
        public ColumnMissingException(string message) : base(message) { }
        public ColumnMissingException(string message, Exception inner) : base(message, inner) { }
        protected ColumnMissingException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
