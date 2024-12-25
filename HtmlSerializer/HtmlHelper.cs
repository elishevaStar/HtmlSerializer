using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HtmlSerializer
{
    public class HtmlHelper
    {
        private readonly static HtmlHelper _instance = new HtmlHelper();
        public static HtmlHelper Instance => _instance;
        public string[] HtmlTags { get; private set; }
        public string[] HtmlVoidTags { get; private set; }

        private HtmlHelper()
        {
            //טעינת מקבצי הJSON
            LoadTags();
        }
        private void LoadTags()
        {
            try
            {
                string htmlTagsJson = File.ReadAllText("JSON_Files/HtmlTags.json");
                string htmlVoidTagsJson = File.ReadAllText("JSON_Files/HtmlVoidTags.json");
                HtmlTags = JsonSerializer.Deserialize<string[]>(htmlTagsJson);
                HtmlVoidTags = JsonSerializer.Deserialize<string[]>(htmlVoidTagsJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"שגיאה בטעינת קבצים: {ex.Message}");
            }
        }
    }
}
