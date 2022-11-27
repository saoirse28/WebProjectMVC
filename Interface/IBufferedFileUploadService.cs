using Microsoft.AspNetCore.WebUtilities;

namespace WebProjectMVC.Interface
{
    public interface IBufferedFileUploadService
    {
        Task<bool> UploadFile(IFormFile file);
    }
}
