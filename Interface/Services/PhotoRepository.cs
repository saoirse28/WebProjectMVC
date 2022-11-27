using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Security.Claims;
using WebProjectMVC.Data;
using WebProjectMVC.Hubs;
using WebProjectMVC.Models;
using WebProjectMVC.ViewModels;

namespace WebProjectMVC.Interface.Services
{
    public class PhotoRepository : IPhotos
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _claimUser;
        private readonly IReactions _reactions;
        public PhotoRepository(ApplicationDbContext context,
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

        public async Task<PhotoViewModel> PhotoListAsync()
        {
            var applicationDbContext = _context.Photos
                .Include(p => p.User)
                .Include(d => d.PhotoDetails)
                .Include(pr=>pr.PhotoReactions)
                .OrderByDescending(o => o.Id);

            PhotoViewModel photoViewModel = new()
            {
                PhotoList = await applicationDbContext.ToListAsync(),
                ReactionRef = await _reactions.ReactionListAsync()
            };

            return photoViewModel;
        }
        public async Task<PhotoViewModel> GetItemAsync(int? id)
        {

            PhotoViewModel photoViewModel = new()
            {
                Photo = await _context.Photos
                    .Include(p => p.User)
                    .Include(pr=>pr.PhotoReactions)
                    .Include(d => d.PhotoDetails)
                        .ThenInclude(r => r.PhotoDetailReactions)
                            .ThenInclude(u => u.User)
                    .Include(pd => pd.PhotoDetails)
                        .ThenInclude(c=>c.PhotoDetailComments)
                            .ThenInclude(u=>u.User)
                    .FirstOrDefaultAsync(m => m.Id == id),
                ReactionRef = await _reactions.ReactionListAsync()
            };
            return photoViewModel;
        }

        public async Task<bool> CreateAsync(Photo photo)
        {
            var user = await _userManager.GetUserAsync(_claimUser);
            Photo newPhoto = new()
            {
                Title = photo.Title,
                Description = photo.Description,
                User = user
            };

            List<PhotoDetail> photoList = new List<PhotoDetail>();
            if (photo.Files.Count > 0)
            {
                foreach (var formFile in photo.Files)
                {
                    if (formFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await formFile.CopyToAsync(memoryStream);
                            if (memoryStream.Length < 2097152)
                            {
                                var newphoto = new PhotoDetail()
                                {
                                    Bytes = memoryStream.ToArray(),
                                    Description = formFile.FileName,
                                    FileExtension = Path.GetExtension(formFile.FileName),
                                    Size = formFile.Length,
                                };
                                photoList.Add(newphoto);
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            newPhoto.PhotoDetails = photoList;
            _context.Photos.Add(newPhoto);
            await _context.SaveChangesAsync();


            return true;
        }

        public async Task<PhotoViewModel> EditViewAsync(int? id)
        {

            PhotoViewModel photoViewModel = new()
            {
                Photo = await _context.Photos
                .Include(d => d.PhotoDetails)
                    .ThenInclude(c => c.PhotoDetailComments)
                        .ThenInclude(du=>du.User)
                .Include(u => u.User)
                .Include(pr=>pr.PhotoReactions)
                .FirstOrDefaultAsync(m => m.Id == id),
                ReactionRef = await _reactions.ReactionListAsync()
            };

            return photoViewModel;
        }
        public async Task<Photo> EditPostAsync(Photo photo)
        {
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            _context.Update(photo);
            _context.Entry(photo).Property(f => f.ApplicationUserId).IsModified = false;
            await _context.SaveChangesAsync();
            return photo;
        }
        public async Task<Photo> DeleteItemAsync(int? id)
        {
            var photo = await _context.Photos
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            return photo;
        }
        public async Task<bool> DeleteConfirmedAsync(int id)
        {
            var photo = await _context.Photos.FindAsync(id);
            if (photo != null)
            {
                _context.Photos.Remove(photo);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        private bool PhotoExists(int id)
        {
            return _context.Photos.Any(e => e.Id == id);
        }
    }
}
