using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuroImport.Exceptions
{
    class JsonHeaderNameEmptyException : Exception
    {
        public JsonHeaderNameEmptyException() { }
        public JsonHeaderNameEmptyException(string message) : base(message) { }
        public JsonHeaderNameEmptyException(string message, Exception inner) : base(message, inner) { }
        protected JsonHeaderNameEmptyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
