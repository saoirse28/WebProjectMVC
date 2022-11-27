using Microsoft.AspNetCore.Mvc;
using WebProjectMVC.Models;
using WebProjectMVC.ViewModels;

namespace WebProjectMVC.Interface
{
    public interface IPhotoDetailReactions
    {
        public Task<bool>TagPhotoDetailReaction(PhotoDetailReactions photoReactions);
    }
}
