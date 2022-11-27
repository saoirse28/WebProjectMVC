using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebProjectMVC.Data;
using WebProjectMVC.Models;
using WebProjectMVC.ViewModels;
using System.Linq;


namespace WebProjectMVC.Interface.Services
{
    public class PhotoDetailRepository : IPhotoDetails
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _claimUser;
        private readonly IReactions _reactions;
        public PhotoDetailRepository(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor httpContextAccessor,
            IReactions reactions)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _claimUser = httpContextAccessor.HttpContext.User;
            _reactions=reactions;
        }

        public Task<bool> CreateAsync(PhotoViewModel reaction)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteConfirmedAsync(int id)
        {
            var photoDetail = await _context.PhotoDetail.FindAsync(id);
            var PhotoId = 0;

            if (photoDetail != null)
            {
                PhotoId =  photoDetail.PhotoId;
                _context.PhotoDetail.Remove(photoDetail);
                _context.SaveChanges();

                var photo = await _context.Photos
                    .Include(pd=>pd.PhotoDetails)
                    .FirstOrDefaultAsync(m => m.Id == PhotoId);
                if (photo!= null)
                {
                    if (photo.PhotoDetails.Count() == 0)
                    {
                        _context.Photos.Remove(photo);
                        _context.SaveChanges();
                        PhotoId=0;
                    }
                }
            }
            return PhotoId;
        }

        public async Task<PhotoViewModel> DeleteItemAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<PhotoViewModel> EditPostAsync(PhotoViewModel reaction)
        {
            throw new NotImplementedException();
        }

        public Task<PhotoViewModel> EditViewAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<PhotoViewModel> GetItemAsync(int? id)
        {
            PhotoViewModel photoViewModel = new()
            {
                PhotoDetail = await _context.PhotoDetail
                .Include(p => p.Photo).ThenInclude(u=>u.User)
                .Include(c => c.PhotoDetailComments).ThenInclude(u => u.User)
                .Include(r => r.PhotoDetailReactions).ThenInclude(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id),
                ReactionRef = await _reactions.ReactionListAsync()
            };

            var myDetail = from photo in _context.Photos
                           join photodetail in _context.PhotoDetail
                            on photo.Id equals photodetail.PhotoId
                            select photodetail;

            return photoViewModel;
        }

        public Task<List<PhotoViewModel>> PhotoDetailListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
