using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huge.Prerender.Core.DataService
{
    public static class DataServiceFactory
    {
        public static IDataService GetDataService(Models.Enums.StorageType storageType)
        {
            IDataService dataService;
            switch (storageType)
            {
                case Models.Enums.StorageType.File:
                    dataService = new FileDataService();
                    break;
                case Models.Enums.StorageType.MongoDB:
                    dataService = new MongoDataService();
                    break;
                default:
                    dataService = new FileDataService();
                    break;
            }
            return dataService;
        }
    }
}
