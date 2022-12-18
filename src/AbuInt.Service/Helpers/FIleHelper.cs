using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbuInt.Service.Helpers;

public class FIleHelper
{
    ///<summary>
    /// This method th file save
    ///<summary>
    public async Task<(string fileName, string filePath )> SaveAsync(IFormFile file, FileType fileType = FileType.Image)
    {
        string fileName = ImageHelper.MakeImageName(file.FileName);
        
        string path;
        string partPath;
        if (fileType == FileType.Image)
        {
            partPath = Path.Combine(EnvironmentHelper.AttachmentPath, fileName);
            path = Path.Combine(EnvironmentHelper.Attachment, partPath);
            
            if (!Directory.Exists(EnvironmentHelper.AttachmentPath))
                Directory.CreateDirectory(EnvironmentHelper.AttachmentPath);
        }
        else
        {
            partPath = Path.Combine(EnvironmentHelper.FilePath, fileName);
            path = Path.Combine(EnvironmentHelper.File, partPath);

            if (!Directory.Exists(EnvironmentHelper.FilePath))
                Directory.CreateDirectory(EnvironmentHelper.FilePath);
        }


        var stream = File.Create(path);
        await file.CopyToAsync(stream);
        stream.Close();

        return (fileName, partPath);
    }

    ///<summary>
    /// This method th file delete
    ///<summary>
    public Task<bool> DeleteAsync(string relativeImagePath, FileType fileType = FileType.Image)
    {
        string absoluteFilePath;
        if (fileType == FileType.Image)
            absoluteFilePath = Path.Combine(EnvironmentHelper.AttachmentPath, relativeImagePath);
        else 
            absoluteFilePath = Path.Combine(EnvironmentHelper.FilePath, relativeImagePath);

        if (!File.Exists(absoluteFilePath)) return Task.FromResult(false);

        try
        {
            File.Delete(absoluteFilePath);
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }
}

public enum FileType
{
    Image,
    File
}
