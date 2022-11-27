using Microsoft.AspNetCore.WebUtilities;

namespace WebProjectMVC.Interface
{
    public interface IStreamFileUploadService
    {
        Task<bool> UploadFile(MultipartReader reader, MultipartSection section);
    }
}
