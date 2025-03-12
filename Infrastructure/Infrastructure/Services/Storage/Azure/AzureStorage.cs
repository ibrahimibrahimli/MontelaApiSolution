using Application.Abstractions.Storage.Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.Storage.Azure
{
    public class AzureStorage : Storage, IAzureStorage
    {
        private readonly BlobServiceClient _serviceClient;
        BlobContainerClient _containerClient;

        public AzureStorage(IConfiguration configuration)
        {
            _serviceClient = new(configuration["Storage:Azure"]);
        }
        public async void Delete(string containerName, string fileName)
        {
            _containerClient = _serviceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = _containerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }

        public List<string> GetFiles(string containerName)
        {
            _containerClient = _serviceClient.GetBlobContainerClient(containerName);
            return _containerClient.GetBlobs().Select(b =>  b.Name).ToList();
        }

        public bool HasFiles(string containerName, string fileName)
        {
            _containerClient = _serviceClient.GetBlobContainerClient(containerName);
            return _containerClient.GetBlobs().Any(b => b.Name == fileName);
        }

        public async Task<List<(string fileName, string pathOrContainer)>> UploadAsync(string containerName, IFormFileCollection files)
        {
            _containerClient = _serviceClient.GetBlobContainerClient(containerName);

            if (!await _containerClient.ExistsAsync())
            {
                await _containerClient.CreateAsync();
               // await _containerClient.SetAccessPolicyAsync(PublicAccessType.Blob);
            }
            List<(string fileName, string containerName)> datas = new();

            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(containerName, file.FileName, HasFiles);

                BlobClient blobClient = _containerClient.GetBlobClient(fileNewName);
                await blobClient.UploadAsync(file.OpenReadStream());
                datas.Add((fileNewName, containerName));
            }
            return datas;
        }
    }
}
