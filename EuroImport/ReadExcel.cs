using EuroImport.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace EuroImport
{
    public class ReadExcel
    {
        string headerNameFile;
        public ReadExcel(string headerNameFile)
        {
            this.headerNameFile = headerNameFile;
        }
        public IEnumerable<Inventory> ReadExcelToTable(string path)
        {

            //Connection String

            string connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0 Xml;HDR=NO;IMEX=1;';";
            
            using (OleDbConnection conn = new OleDbConnection(connstring))
            {
                conn.Open();
                //Get All Sheets Name
                DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });

                //Get the First Sheet Name
                string firstSheetName = "ראשי$";

                //Query String 
                string sql = string.Format("SELECT * FROM [{0}]", firstSheetName);
                OleDbDataAdapter ada = new OleDbDataAdapter(sql, connstring);
                DataSet set = new DataSet();
                ada.Fill(set);
                List<Inventory> inventories = new List<EuroImport.Inventory>();
                HeaderNames headersNamesFromJson = LoadExcelColumnNames.Load(headerNameFile);
                ValidateHeaders(set.Tables[0], headersNamesFromJson);
                FixColumnHeaders(set.Tables[0]);
                for (int index = 1; index < set.Tables[0].Rows.Count; index++)
                {
                    inventories.Add(new Inventory
                    {
                        SKU = set.Tables[0].Rows[index][headersNamesFromJson.SKU].ToString(),
                        GENDER = set.Tables[0].Rows[index][headersNamesFromJson.Gender].ToString(),
                        CATEGORY = set.Tables[0].Rows[index][headersNamesFromJson.Category].ToString(),
                        SUB_CATEGORY = set.Tables[0].Rows[index][headersNamesFromJson.SubCategory].ToString(),
                        BRAND = set.Tables[0].Rows[index][headersNamesFromJson.Brand].ToString(),
                        MODEL = set.Tables[0].Rows[index][headersNamesFromJson.Model].ToString(),
                        MODEL_NAME = set.Tables[0].Rows[index][headersNamesFromJson.ModelName].ToString(),
                        COLOR = set.Tables[0].Rows[index][headersNamesFromJson.Color].ToString(),
                        SIZE = set.Tables[0].Rows[index][headersNamesFromJson.Size].ToString(),
                        REGULAR_PRICE = set.Tables[0].Rows[index][headersNamesFromJson.RegularPrice].ToString(),
                        SALE_PRICE = set.Tables[0].Rows[index][headersNamesFromJson.SalePrice].ToString(),
                        STOCK = set.Tables[0].Rows[index][headersNamesFromJson.Stock].ToString(),
                        REMARKS = set.Tables[0].Rows[index][headersNamesFromJson.Remarks].ToString(),
                        ADDITION_CATEGORY = set.Tables[0].Rows[index][headersNamesFromJson.AdditionCategory].ToString(),
                        LOCATION = set.Tables[0].Rows[index][headersNamesFromJson.Location].ToString()
                    });
                }
                return inventories.Where(c=>string.IsNullOrEmpty(c.MODEL) == false 
                        || string.IsNullOrEmpty(c.COLOR) == false 
                        || string.IsNullOrEmpty(c.SIZE) == false);
            }
        }

        private void FixColumnHeaders(DataTable dataTable)
        {
            var headers = dataTable.Rows[0].ItemArray;
            for (int index = 0; index < dataTable.Columns.Count; index++)
            {
                var value = headers[index].ToString();
                if(string.IsNullOrEmpty(value) == false)
                    dataTable.Columns[index].ColumnName = value;
            }
        }
        private void ValidateHeaders(DataTable dataTable, HeaderNames names)
        {
            var properties = names.GetType().GetProperties();
            foreach (var property in properties)
            {
                var name = property.GetValue(names, new object[] { }).ToString();
                var findName = dataTable.Rows[0].ItemArray.FirstOrDefault(i => i.Equals(name) == true);
                if (findName == null)
                    throw new ColumnMissingException(name);
            }
        }
    }
}
