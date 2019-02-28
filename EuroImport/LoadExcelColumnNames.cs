using EuroImport.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EuroImport
{
    public class HeaderNames
    {
        public string SKU { get; set; }
        public string Gender { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ModelName { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string RegularPrice { get; set; }
        public string SalePrice { get; set; }
        public string Stock { get; set; }
        public string Remarks { get; set; }
        public string AdditionCategory { get; set; }
        public string Location { get; set; }
    }
    public class LoadExcelColumnNames
    {
        public static HeaderNames Load(string fileFullPath)
        {
            JsonReader reader = new JsonReader();
            var headerNames = reader.ReadHeaderNames(fileFullPath);
            Validate(headerNames);
            return headerNames;
        }

        public static void Validate(HeaderNames names)
        {
            var properties = names.GetType().GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(names, new object[] { });
                if (value == null || string.IsNullOrEmpty(value.ToString().Trim()) == true)
                    throw new JsonHeaderNameEmptyException(property.Name);
            }
        }


    }
}
