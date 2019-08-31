using System.IO;
using System.Threading.Tasks;
using crmSeries.Core.Features.FileStorage;

namespace crmSeries.Core.Tests.Mocks
{
    public class MockFileStorageProvider : IFileStorageProvider
    {
        public bool GenerateMockPath { get; set; } = true;

        public string Root => "Root/";

        public Task<string> StoreFileAsync(string container, string fileName, Stream stream, string mimeType)
        {
            var path = !GenerateMockPath ? string.Empty : $"{Root}{container}/{fileName}";

            return Task.FromResult(path);
        }
    }
}
