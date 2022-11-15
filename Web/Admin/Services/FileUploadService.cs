using Admin.Services.Interfaces;
using Microsoft.AspNetCore.Components.Forms;

namespace Admin.Services;

public class FileUploadService : IFileUploadService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileUploadService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task<string> UploadFile(IBrowserFile file)
    {
        var fileInfo = new FileInfo(file.Name);
        // maybe guid to prevent ducplciate name issue
        // var fileName = fileInfo.Name + fileInfo.Extension;
        var fileName = fileInfo.Name;
        var directory = $"{_webHostEnvironment.WebRootPath}\\images\\products";

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var filePath = Path.Combine(directory, fileName);

        await using FileStream fs = new FileStream(filePath, FileMode.Create);
        await file.OpenReadStream().CopyToAsync(fs);

        var fullPath = $"/images/products/{fileName}";
        return fullPath;
    }

    public bool DeleteFile(string path)
    {
        var fullPath = _webHostEnvironment.WebRootPath + path;

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            return true;
        }
        return false;
    }
}