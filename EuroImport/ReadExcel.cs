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
        public static List<string> FieldsNames = new List<string> {"קוד פריט", "מחלקה", "קבוצה", "קבוצת משנה", "ספק", "דגם", "שם דגם", "צבע", "שם מידה", "מחיר 1", "מחיר 2", "יתרת מלאי", "הערות","קטגוריות נוספות","מיקום" };
        protected Dictionary<string, int> columnIndexDictionary = new Dictionary<string, int>();
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
                string firstSheetName = "ראשי$";// sheetsName.Rows[0][2].ToString();

                //Query String 
                string sql = string.Format("SELECT * FROM [{0}]", firstSheetName);
                OleDbDataAdapter ada = new OleDbDataAdapter(sql, connstring);
                DataSet set = new DataSet();
                ada.Fill(set);
                CheckColumnsExist(set.Tables[0]);
                List<Inventory> inventories = new List<EuroImport.Inventory>();
                for (int index = 1; index < set.Tables[0].Rows.Count; index++)
                {
                    inventories.Add(new Inventory
                    {
                        SKU = set.Tables[0].Rows[index][columnIndexDictionary["קוד פריט"]].ToString(),
                        GENDER = set.Tables[0].Rows[index][columnIndexDictionary["מחלקה"]].ToString(),
                        CATEGORY = set.Tables[0].Rows[index][columnIndexDictionary["קבוצה"]].ToString(),
                        SUB_CATEGORY = set.Tables[0].Rows[index][columnIndexDictionary["קבוצת משנה"]].ToString(),
                        //MAIN_GROUP = set.Tables[0].Rows[index]["קבוצת איחוד"].ToString(),
                        BRAND = set.Tables[0].Rows[index][columnIndexDictionary["ספק"]].ToString(),
                        MODEL = set.Tables[0].Rows[index][columnIndexDictionary["דגם"]].ToString(),
                        MODEL_NAME = set.Tables[0].Rows[index][columnIndexDictionary["שם דגם"]].ToString(),
                        COLOR = set.Tables[0].Rows[index][columnIndexDictionary["צבע"]].ToString(),
                        SIZE = set.Tables[0].Rows[index][columnIndexDictionary["שם מידה"]].ToString(),
                        REGULAR_PRICE = set.Tables[0].Rows[index][columnIndexDictionary["מחיר 1"]].ToString(),
                        SALE_PRICE = set.Tables[0].Rows[index][columnIndexDictionary["מחיר 2"]].ToString(),
                        STOCK = set.Tables[0].Rows[index][columnIndexDictionary["יתרת מלאי"]].ToString(),
                        REMARKS = set.Tables[0].Rows[index][columnIndexDictionary["הערות"]].ToString(),
                        ADDITION_CATEGORY = set.Tables[0].Rows[index][columnIndexDictionary["קטגוריות נוספות"]].ToString(),
                        LOCATION = set.Tables[0].Rows[index][columnIndexDictionary["מיקום"]].ToString()
                    });
                }
                return inventories.Where(c=>string.IsNullOrEmpty(c.MODEL) == false 
                        || string.IsNullOrEmpty(c.COLOR) == false 
                        || string.IsNullOrEmpty(c.SIZE) == false);
            }
        }

        private void CheckColumnsExist(DataTable dataTable)
        {
            foreach (var columnName in FieldsNames)
            {
                if (dataTable.Rows[0].ItemArray.FirstOrDefault(c=>c.ToString().Contains(columnName)) == null)
                    throw new ColumnMissingException(columnName);
            }

            for (int index = 0; index < dataTable.Rows[0].ItemArray.Length; index++)
            {
                string columnName = dataTable.Rows[0].ItemArray[index].ToString();
                columnIndexDictionary.Add(columnName, index);
            }
        }
    }
}
