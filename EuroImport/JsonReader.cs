using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace EuroImport
{
    public class JsonReader
    {
        public Dictionary<string, string> Read(string url)
        {
            using (WebClient wc = new WebClient { Encoding = Encoding.UTF8})
            {
                var json = wc.DownloadString(url);
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
        }
    }
}
