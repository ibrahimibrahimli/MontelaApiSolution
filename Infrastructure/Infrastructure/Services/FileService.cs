using Application.Services;
using Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO.Pipelines;

namespace Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await fileStream.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;

            }
        }

         async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
        {
            string fileNewName = await Task.Run<string>(async() =>
            {
                string extension = Path.GetExtension(fileName);

                string fileNewName = string.Empty;
                if (first)
                {
                    string oldName = Path.GetFileNameWithoutExtension(fileName);
                    fileNewName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
                }
                else
                {
                    fileNewName = fileName;
                    int indexNo1 = fileNewName.IndexOf("-");

                    if(indexNo1 == -1)
                    {
                        fileNewName = $"{Path.GetFileNameWithoutExtension(fileName)}-2{extension}";
                    }
                    else
                    {
                        int indexNo2 = fileNewName.IndexOf(".");
                        string fileNo = fileNewName.Substring(indexNo1, indexNo2 - indexNo1 - 1);
                        int _fileNo = int.Parse(fileNo);
                        _fileNo++;
                        fileNewName = fileNewName.Remove(indexNo1, indexNo2 - indexNo1 - 1)
                        .Insert(indexNo1, _fileNo.ToString());
                    }
                }



                if (File.Exists($"{path}\\{fileNewName}"))
                    return await FileRenameAsync(path, fileNewName, false);
                else
                    return fileNewName;
            });
            return fileNewName;
        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_env.WebRootPath, path);
            if (Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas = new();
            List<bool> results = new();
            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(file.FileName);
                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                datas.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
                results.Add(result);
            }
            if(results.TrueForAll(x => x.Equals(true))){
               return datas;
            }


            return null;
        }
    }
}
