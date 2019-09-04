using System.IO;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.FileStorage
{
    public class LocalFileStorageProvider : IFileStorageProvider
    {
        private readonly string _RootPath;

        public LocalFileStorageProvider(string root, string publicUrl)
        {
            Root = $"{publicUrl}/FileStorage/";
            _RootPath = Path.Combine(root, "FileStorage");
        }

        public string Root { get; }

        public Task<string> StoreFileAsync(string container, string fileName, Stream stream, string mimeType)
        {
            var containerPath = Path.Combine(_RootPath, container);

            if (!Directory.Exists(containerPath))
                Directory.CreateDirectory(containerPath);

            var filePath = Path.Combine(containerPath, fileName);

            using (var fileStream = File.Create(filePath))
            {
                if (stream.CanSeek)
                    stream.Seek(0, SeekOrigin.Begin);

                stream.CopyTo(fileStream);
            }

            return Task.FromResult($"{Root}{container}/{fileName}");
        }
    }
}
