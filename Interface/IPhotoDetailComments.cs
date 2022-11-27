using WebProjectMVC.Models;
using WebProjectMVC.ViewModels;

namespace WebProjectMVC.Interface
{
    public interface IPhotoDetailComments
    {
        public Task<List<PhotoDetailComment>> PhotoDetailListAsync();
        public Task<PhotoDetailComment> GetItemAsync(int? id);
        public Task<bool> CreateAsync(PhotoDetailComment photoDetailComment);
        public Task<PhotoDetailComment> EditViewAsync(int? id);
        public Task<PhotoDetailComment> EditPostAsync(PhotoDetailComment photoDetailComment);
        public Task<PhotoDetailComment> DeleteItemAsync(int? id);
        public Task<int> DeleteConfirmedAsync(int id);
    }
}
