using EuroImport.Exceptions;
using EuroImport.Properties;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroImport
{
    public class ExportCSV
    {
        const string DELIMETER = ",";
        protected Dictionary<string, int> modelImages;
        protected Dictionary<string, string> slugs;
        protected DataValidation dataValidation;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public ExportCSV()
        {
        }
        protected Dictionary<string, string> GetSlugs()
        {
            JsonReader reader = new EuroImport.JsonReader();
            return reader.Read(Settings.Default.SlugURL);
        }
        public string CreateCSV(IEnumerable<Inventory> data, Dictionary<string, int> modelImages, ref List<string> missingImages)
        {
            this.slugs = GetSlugs();
            this.dataValidation = new EuroImport.DataValidation(this.slugs);
            this.modelImages = modelImages;
            string csv = "sku,product_type,post_title,model,regular_price,sale_price,stock_status,shipping_class,featured,stock,post_parent,variations,pa_color,pa_size,pa_width,pa_gender,pa_subgroup,pa_scategory,pa_maingroup,pa_brand,pa_location,color,default_attributes,category,tag,brand,menu_order,reviews,product_gallery,featured_image";

            csv += Environment.NewLine;
            this.dataValidation.Validate(data);
            var modelGrouped = data.AsEnumerable()
                .GroupBy(c => c.PARENT);
            foreach (var item in modelGrouped)
            {
                for (int index = 0; index < item.Count(); index++)
                {
                    try
                    {
                        string image = item.ElementAt(index).PARENT.Replace("-", "");
                        if (modelImages.ContainsKey(image) == true)
                        {
                            item.ElementAt(index).IMAGE_COUNT = modelImages[image].ToString();
                            if (index == 0)
                            {
                                csv += BuildCSVRow(item.ElementAt(index), true);
                                csv += Environment.NewLine;
                            }
                            csv += BuildCSVRow(item.ElementAt(index), false);
                            csv += Environment.NewLine;
                        }
                        else
                        {
                            missingImages.Add(image);
                            logger.Warn("Cannot find image '{0}'", image);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Error at export "+  ex.ToString());
                        throw;
                    }
                }
            }
            return csv;
        }
        public string BuildCSVRow(Inventory inventory, bool isMaster)
        {
            List<string> result = new List<string>();
            if (isMaster == true)
            {
                result.Add(inventory.PARENT);
                result.Add("variation_master");
            }
            else
            {
                result.Add(inventory.SKU);
                result.Add("product_variation");
            }

            //            result.Add((inventory.MODEL_NAME.Replace(inventory.MODEL,"") + " " + inventory.PARENT).ToUpperInvariant());
            //           result.Add(inventory.MODEL_NAME.Replace(inventory.MODEL, "").ToUpperInvariant());
            result.Add(inventory.MODEL_NAME.ToUpperInvariant());
            result.Add(inventory.MODEL_NAME.Replace(inventory.PARENT, "").ToUpperInvariant());

            result.Add(inventory.REGULAR_PRICE);
            result.Add(inventory.SALE_PRICE);
            
            result.Add((inventory.STOCK != "0" && string.IsNullOrEmpty(inventory.STOCK) == false ? "instock" : "outstock") );
            result.Add("normal_shipping");
            result.Add("no");
            result.Add(inventory.STOCK);
            result.Add(inventory.PARENT);
            result.Add("gender->0->0|subgroup->0->0|scategory->0->0|maingroup->0->0|brand->0->0|location->0->0|size->1->1");
            result.Add("");
            result.Add(inventory.SIZE);
            result.Add("");
            result.Add(GetSlugValue(inventory.GENDER));
            result.Add(GetSlugValue(inventory.CATEGORY));
            result.Add(GetSlugValue(inventory.SUB_CATEGORY));
            //result.Add(GetSlugValue("קבוצת איחוד", inventory.MAIN_GROUP));
            result.Add("");
            
            result.Add(inventory.BRAND.TileCase());
            result.Add(inventory.LOCATION);
            result.Add(inventory.COLOR);
            result.Add("");//default_attributes
            
            result.Add(CombinesCategories(inventory.GENDER, inventory.BRAND.ToLowerInvariant(), inventory.ADDITION_CATEGORY,inventory.CATEGORY,inventory.SUB_CATEGORY));//category
            result.Add(inventory.GENDER + "|" + inventory.BRAND.ToLowerInvariant() 
                + "|" + inventory.MODEL_NAME.ToUpperInvariant() + "|" + inventory.MODEL);//tags
            result.Add(inventory.BRAND.ToLowerInvariant());
            result.Add("0");
            result.Add(inventory.REMARKS);
            result.Add(BuildImagesName(inventory.PARENT, inventory.IMAGE_COUNT));
            result.Add(BuildImagesName(inventory.PARENT, "1"));
            return string.Join(DELIMETER, result);
        }

        protected string CombinesCategories(params string[] categories)
        {
            SplitValues spliter = new EuroImport.SplitValues(Settings.Default.Delimeter);
            var splitesValues = categories.SelectMany(c => spliter.Split(c));
            
            CombineValues combines = new EuroImport.CombineValues(Settings.Default.Delimeter);
            return combines.Combines(splitesValues
                .Where(c => string.IsNullOrEmpty(c.Trim()) == false));
        }

        protected string BuildImagesName(string parent, string imageCount)
        {
            parent = parent.Replace("-", "");
            string images = string.Empty;
            int count = Convert.ToInt32(imageCount);
            for (int index = 0; index < count; index++)
            {
                string delimeter = string.Empty;
                if (index > 0)
                    delimeter = "|";
                images += string.Format("{0}{1}_{2}.jpg",delimeter, parent, index + 1);
            }
            return images;
        }
        protected string GetSlugValue(string value)
        {
            return slugs[value];
        }
    }
}
