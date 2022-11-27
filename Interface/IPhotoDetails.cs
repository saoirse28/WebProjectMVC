using WebProjectMVC.Models;
using WebProjectMVC.ViewModels;

namespace WebProjectMVC.Interface
{
    public interface IPhotoDetails
    {
        public Task<List<PhotoViewModel>> PhotoDetailListAsync();
        public Task<PhotoViewModel> GetItemAsync(int? id);
        public Task<bool> CreateAsync(PhotoViewModel reaction);
        public Task<PhotoViewModel> EditViewAsync(int? id);
        public Task<PhotoViewModel> EditPostAsync(PhotoViewModel reaction);
        public Task<PhotoViewModel> DeleteItemAsync(int? id);
        public Task<int> DeleteConfirmedAsync(int id);
    }
}
