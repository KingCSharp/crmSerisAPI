using System.IO;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.FileStorage
{
    public interface IFileStorageProvider
    {
        string Root { get; }

        Task StoreFile(string container, string fileName, Stream stream, string mimeType);
    }
}
