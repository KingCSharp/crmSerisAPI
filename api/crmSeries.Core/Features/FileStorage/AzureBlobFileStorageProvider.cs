using System;
using System.IO;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.FileStorage
{
    public class AzureBlobFileStorageProvider : IFileStorageProvider
    {
        public string Root => throw new NotImplementedException();

        public Task StoreFile(string container, string fileName, Stream stream, string mimeType)
        {
            throw new NotImplementedException();
        }
    }
}
