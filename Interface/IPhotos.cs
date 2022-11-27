using WebProjectMVC.Models;
using WebProjectMVC.ViewModels;

namespace WebProjectMVC.Interface
{
    public interface IPhotos
    {
        public Task<PhotoViewModel> PhotoListAsync();
        public Task<PhotoViewModel> GetItemAsync(int? id);
        public Task<bool> CreateAsync(Photo photo);
        public Task<PhotoViewModel> EditViewAsync(int? id);
        public Task<Photo> EditPostAsync(Photo photo);
        public Task<Photo> DeleteItemAsync(int? id);
        public Task<bool> DeleteConfirmedAsync(int id);
    }
}
