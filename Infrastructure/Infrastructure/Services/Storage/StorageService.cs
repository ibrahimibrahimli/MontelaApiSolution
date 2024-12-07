using Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        private readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName { get => _storage.GetType().Name; }

        public void Delete(string pathOrContainerName, string fileName)
            => _storage.Delete(pathOrContainerName, fileName);

        public List<string> GetFiles(string pathOrContainerName)
            => _storage.GetFiles(pathOrContainerName);

        public bool HasFiles(string pathOrContainerName, string fileName)
            => _storage.HasFiles(pathOrContainerName, fileName);

        public Task<List<(string fileName, string pathOrContainer)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
            => _storage.UploadAsync(pathOrContainerName, files);
    }
}
