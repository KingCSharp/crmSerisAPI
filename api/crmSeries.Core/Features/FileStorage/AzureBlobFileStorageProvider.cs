using System;
using System.IO;
using System.Threading.Tasks;
using crmSeries.Core.Security;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace crmSeries.Core.Features.FileStorage
{
    public class AzureBlobFileStorageProvider : IFileStorageProvider
    {
        private readonly IIdentityApiContext _apiIdentity;

        private string _connString = string.Empty;
        private CloudStorageAccount _cloudStorageAccount;

        public AzureBlobFileStorageProvider(IIdentityApiContext apiIdentity)
        {
            _apiIdentity = apiIdentity;
        }

        public string Root
        {
            get
            {
                var user = _apiIdentity.RequestingUser;
                if (user == null)
                    return string.Empty;

                var connString = CreateConnectionString(user.StorageAccount, user.StorageAccountKey);
                if (connString != _connString && CloudStorageAccount.TryParse(connString, out _cloudStorageAccount))
                {
                    _connString = connString;
                }

                return _cloudStorageAccount?.BlobEndpoint.ToString();
            }
        }
        
        public async Task<string> StoreFileAsync(string container, string fileName, Stream stream, string mimeType)
        {
            var root = Root;
            if (_cloudStorageAccount == null || string.IsNullOrEmpty(root))
                throw new InvalidOperationException("Could not connect to user storage account.");
            
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

            return $"{root}{container}/{fileName}";
        }

        private static string CreateConnectionString(string storageAccount, string key)
            => $"DefaultEndpointsProtocol=https;AccountName={storageAccount};AccountKey={key};EndpointSuffix=core.windows.net";
    }
}
