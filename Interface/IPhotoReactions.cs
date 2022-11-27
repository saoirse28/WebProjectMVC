using Microsoft.AspNetCore.Mvc;
using WebProjectMVC.Models;
using WebProjectMVC.ViewModels;

namespace WebProjectMVC.Interface
{
    public interface IPhotoReactions
    {
        public List<PhotoReactionCount> GetPhotoReaction(PhotoReactions photoReactions);
        public Task<bool>TagPhotoReaction(PhotoReactions photoReactions);
    }
}
