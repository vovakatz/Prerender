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
            File.WriteAllText(baseFolder + websiteKey + "\\" + GetFileName(url), content);
        }

        private string GetFileName(string url)
        {
            string fileName = url.Replace('\\', '_')
                .Replace('/', '_')
                .Replace(':', '_')
                .Replace('*', '_')
                .Replace('?', '_')
                .Replace('"', '_')
                .Replace('<', '_')
                .Replace('>', '_')
                .Replace('|', '_');
            return fileName + ".html";
        }
    }
}
