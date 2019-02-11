using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroImport
{
    public class Inventory
    {

        string sku, gender, category, sub_category,
                model, model_name, color, size, main_group,remarks,
                regular_price, sale_price, stock, brand, imageCount, 
                addition_category, location;

        public Inventory()
        {
            sku = gender = category = sub_category =
                model = model_name = color = size = main_group =
                regular_price = sale_price = stock = brand =
                imageCount = remarks = addition_category = location = string.Empty;
        }
        public string SKU { get {return sku.ToUpperInvariant(); } set {sku = value.Trim(); } }
        public string GENDER { get { return gender; } set { gender = value.Trim(); } }
        public string CATEGORY { get { return category; } set { category = value.Trim(); } }
        public string SUB_CATEGORY { get { return sub_category; } set { sub_category = value.Trim(); } }
        public string MAIN_GROUP { get {return main_group; } set { main_group = value.Trim(); } }
        public string MODEL { get { return model.ToUpperInvariant(); } set { model = value.Trim(); } }
        public string MODEL_NAME { get { return model_name; } set { model_name = value.Trim(); } }
        public string COLOR { get { return color; } set { color = value.Trim(); } }
        public string SIZE { get { return size; } set { size = value.Trim(); } }
        public string REGULAR_PRICE { get { return regular_price; } set { regular_price = value.Trim(); } }
        public string SALE_PRICE { get { return sale_price; } set { sale_price = value.Trim(); } }
        public string STOCK { get { return stock; } set { stock = value.Trim(); } }
        public string REMARKS { get { return remarks; } set { remarks = value.Trim(); } }
        public string BRAND { get { return brand; } set { brand = value.Trim(); } }
        public string IMAGE_COUNT { get { return imageCount; } set { imageCount = value.Trim(); } }
        public string PARENT { get { return string.Format("{0}-{1}", this.MODEL, this.COLOR).ToUpperInvariant(); } }
        public string ADDITION_CATEGORY { get { return addition_category; } set { addition_category = value.Trim(); } }
        public string LOCATION { get { return location; } set { location = value.Trim(); } }
    }
}
