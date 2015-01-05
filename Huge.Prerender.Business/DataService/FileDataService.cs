using Huge.Prerender.Core.Utilities;
using System.IO;

namespace Huge.Prerender.Core.DataService
{
    public class FileDataService : IDataService
    {
        string baseFolder = "c:\\Temp\\";

        public void Save(string websiteKey, string url, string content)
        {
            string directoryPath = baseFolder + websiteKey;
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            Common common = new Common();
            File.WriteAllText(baseFolder + websiteKey + "\\" + common.GetFileName(url) + ".html", content);
        }

        public string GetContent(string websiteKey, string url)
        {
            Common common = new Common();
            string filePath = baseFolder + websiteKey + "\\" + common.GetFileName(url) + ".html";
            if (File.Exists(filePath))
                return File.ReadAllText(filePath);
            return null;
        }
    }
}
