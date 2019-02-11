using EuroImport.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuroImport
{
    public class SplitValues
    {
        protected string delimeter;
        protected List<string> noValidDelimeters = new List<string>{ "\"", ",", "|"};
        public SplitValues(string delimeter)
        {
            this.delimeter = delimeter;
            this.noValidDelimeters.Remove(delimeter);
        }
        public IEnumerable<string> Split(string values)
        {
            if(values == null || string.IsNullOrEmpty(values.Trim()))
                return new List<string>();
            if(IsContainsNoValidDelimeter(values) == true)
                throw new WrongDelimeterException(values);
            return values.Split(new string[] { delimeter}, StringSplitOptions.RemoveEmptyEntries);
        }

        protected bool IsContainsNoValidDelimeter(string values)
        {
            return this.noValidDelimeters.Any(c => values.Contains(c) == true);
        }
    }
}
