using Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class FileService
    {
        async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
        {
            string fileNewName = await Task.Run<string>(async () =>
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

                    if (indexNo1 == -1)
                    {
                        fileNewName = $"{Path.GetFileNameWithoutExtension(fileName)}-2{extension}";
                    }
                    else
                    {
                        int lastIndex = 0;
                        while (true)
                        {
                            lastIndex = indexNo1;
                            indexNo1 = fileNewName.IndexOf("-", indexNo1 + 1);
                            if (indexNo1 == -1)
                            {
                                indexNo1 = lastIndex;
                                break;
                            }
                        }

                        int indexNo2 = fileNewName.IndexOf(".");
                        string fileNo = fileNewName.Substring(indexNo1 + 1, indexNo2 - indexNo1 - 1);

                        if (int.TryParse(fileNo, out int _fileNo))
                        {
                            _fileNo++;
                            fileNewName = fileNewName.Remove(indexNo1 + 1, indexNo2 - indexNo1 - 1)
                            .Insert(indexNo1 + 1, _fileNo.ToString());
                        }
                        else
                        {
                            fileNewName = $"{Path.GetFileNameWithoutExtension(fileName)}-2{extension}";
                        }
                    }
                }



                if (File.Exists($"{path}\\{fileNewName}"))
                    return await FileRenameAsync(path, fileNewName, false);
                else
                    return fileNewName;
            });
            return fileNewName;
        }

    }
}
