
namespace Huge.Prerender.Core.DataService
{
    public interface IDataService
    {
        void Save(string websiteKey, string url, string content);
        string GetContent(string websiteKey, string url);
    }
}
