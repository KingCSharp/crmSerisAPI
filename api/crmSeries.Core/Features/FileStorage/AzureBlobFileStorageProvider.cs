using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace crmSeries.Core.Features.FileStorage
{
    public class AzureBlobFileStorageProvider : IFileStorageProvider
    {
        private readonly CloudStorageAccount _cloudStorageAccount;

        public AzureBlobFileStorageProvider(string connectionString)
        {
            if (!CloudStorageAccount.TryParse(connectionString, out _cloudStorageAccount))
                throw new Exception("Failed to connect to Azure Blob account.");
        }

        public string Root => _cloudStorageAccount.BlobEndpoint.ToString();

        public async Task<string> StoreFileAsync(string container, string fileName, Stream stream, string mimeType)
        {
            var blobClient = _cloudStorageAccount.CreateCloudBlobClient();

            var containerReference = blobClient.GetContainerReference(container);
            var blob = containerReference.GetBlockBlobReference(fileName);

            await blob.Container.CreateIfNotExistsAsync(
                BlobContainerPublicAccessType.Blob, 
                new BlobRequestOptions(), 
                new OperationContext());

            if (blob.Exists())
                throw new InvalidOperationException($"{container}/{fileName} already exists.");
            
            blob.Properties.ContentType = mimeType;

            if (stream.CanSeek)
                stream.Seek(0, SeekOrigin.Begin);

            await blob.UploadFromStreamAsync(stream);

            return $"{Root}{container}/{fileName}";
        }
    }
}
