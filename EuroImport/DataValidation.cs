using EuroImport.Exceptions;
using EuroImport.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuroImport
{
    public class DataValidation
    {
        Dictionary<string, string> slugs;
        protected int currentLine = 1;
        protected string currentSKU = string.Empty;
        public DataValidation(Dictionary<string, string> slugs)
        {
            this.slugs = slugs;
        }
        public void Validate(IEnumerable<Inventory> inventories)
        {
            currentLine = 2;
            foreach (var inventory in inventories)
            {
                this.currentSKU = (inventory.PARENT + inventory.SIZE).Replace("-", "").Trim();
                ValidateSlug("מחלקה", inventory.GENDER);
                ValidateSlug("קבוצה", inventory.CATEGORY);
                ValidateSlug("קבוצת משנה", inventory.SUB_CATEGORY);
                ValidateSlug("ספק", inventory.BRAND);
                Validate("דגם", inventory.MODEL);
                if (Settings.Default.ValidateColor == true)
                    Validate("צבע", inventory.COLOR);
                if (Settings.Default.ValidateSize == true)
                    ValidateNumeric("שם מידה", inventory.SIZE);
                ValidateCurrency("מחיר 1", inventory.REGULAR_PRICE);
                ValidateCurrency("מחיר 2", inventory.SALE_PRICE);
                ValidateNumeric("יתרת מלאי", inventory.STOCK);
                ValidateSplit("קטגוריות נוספות", inventory.ADDITION_CATEGORY);
                Validate("מיקום", inventory.LOCATION);
                currentLine++;
            }
        }


        protected void Validate(string columnName, string value)
        {
            if (string.IsNullOrEmpty(value.Trim()) == true)
                throw new ValueMissingException(columnName, value, this.currentLine, currentSKU);
        }

        protected void ValidateNumeric(string columnName, string value)
        {
            double result = 0;
            if (double.TryParse(value, out result) == false)
                throw new ValueNumericException(columnName, value, this.currentLine, currentSKU);
        }

        protected void ValidateSlug(string columnName, string value)
        {
            if (slugs == null)
                throw new LoaderException("Slugs file is missing");
            if (slugs.ContainsKey(value.ToLowerInvariant()) == false)
                throw new SlugsException(columnName, value, this.currentLine, this.currentSKU);
        }
        protected void ValidateCurrency(string columnName, string value)
        {
            ValidateNumeric(columnName, value);
        }

        protected void ValidateSplit(string columnName, string value)
        {
            try
            {
                SplitValues splitter = new SplitValues(Settings.Default.Delimeter);
                var values = splitter.Split(value);
                foreach (var item in values)
                    ValidateSlug(columnName, item);
            }
            catch (WrongDelimeterException ex)
            {
                throw new WrongDelimeterException(columnName, value, this.currentLine, this.currentSKU);
            }
        }
    }
}
