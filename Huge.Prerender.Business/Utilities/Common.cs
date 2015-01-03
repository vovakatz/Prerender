
namespace Huge.Prerender.Core.Utilities
{
    public class Common
    {
        public string GetFileName(string url)
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
            return fileName;
        }
    }
}
