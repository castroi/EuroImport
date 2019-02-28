using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroImport
{
    public class ModelImages
    {
        public Dictionary<string, int> GetModels(string imagesFolder)
        {
            Dictionary<string, int> models = new Dictionary<string, int>();
            var files = Directory.GetFiles(imagesFolder, "*.jpg");
            var grouped = files.OrderBy(c => c).GroupBy(c => c.Split('\\').Last().Split('_').ElementAt(0).ToUpper());
            foreach (var item in grouped)
                models.Add(item.Key.ToUpper(), item.Count());
            return models;
        }
    }
}
