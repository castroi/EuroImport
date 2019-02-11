using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuroImport.Exceptions
{
    [Serializable]
    public class LoaderException : Exception
    {
        public LoaderException() { }
        public LoaderException(string message) : base(message) { }
        public LoaderException(string message, Exception inner) : base(message, inner) { }
        protected LoaderException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
