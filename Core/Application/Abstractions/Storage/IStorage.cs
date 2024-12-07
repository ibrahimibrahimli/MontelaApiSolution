using Microsoft.AspNetCore.Http;

namespace Application.Abstractions.Storage
{
    public interface IStorage
    {
        Task<List<(string fileName, string pathOrContainer)>> UploadAsync(string pathOrContainerName, IFormFileCollection files);
        void Delete(string pathOrContainerName,  string fileName);
        List<string> GetFiles(string pathOrContainerName);
        bool HasFiles(string pathOrContainerName, string fileName);
    }
}
