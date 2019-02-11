using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuroImport
{
    public class CombineValues
    {
        protected string delimeter;
        public CombineValues(string delimeter)
        {
            this.delimeter = delimeter;
        }
        public string Combines(IEnumerable<string> values)
        {
            if(values == null || values.Count() == 0)
                return string.Empty;
            if (values.Count() == 1)
                return values.ElementAt(0);
            return string.Join(delimeter, values.Distinct());
        }
    }
}
