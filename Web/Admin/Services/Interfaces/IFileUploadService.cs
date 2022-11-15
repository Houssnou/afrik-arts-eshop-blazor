using System.Net;
using Microsoft.AspNetCore.Components.Forms;

namespace Admin.Services.Interfaces;

public interface IFileUploadService
{
    Task<string> UploadFile(IBrowserFile file);
    bool DeleteFile(string path);

}