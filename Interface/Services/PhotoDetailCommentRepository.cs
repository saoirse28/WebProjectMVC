using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Security.Claims;
using WebProjectMVC.Data;
using WebProjectMVC.Models;

namespace WebProjectMVC.Interface.Services
{
    public class PhotoDetailCommentRepository : IPhotoDetailComments
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _claimUser;        
        public PhotoDetailCommentRepository(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _claimUser = httpContextAccessor.HttpContext.User;
            
        }

        public async Task<bool> CreateAsync(PhotoDetailComment photoDetailComment)
        {
            _context.Add(photoDetailComment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> DeleteConfirmedAsync(int id)
        {

            var photoDetailComment = await _context.PhotoDetailComment.FindAsync(id);
            if (photoDetailComment == null)
                return 0;
            var photoDetailId = photoDetailComment.PhotoDetailId;
                _context.PhotoDetailComment.Remove(photoDetailComment);
                await _context.SaveChangesAsync();
            return photoDetailId;
        }

        public Task<PhotoDetailComment> DeleteItemAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<PhotoDetailComment> EditPostAsync(PhotoDetailComment photo)
        {
            throw new NotImplementedException();
        }

        public Task<PhotoDetailComment> EditViewAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<PhotoDetailComment> GetItemAsync(int? id)
        {
            throw new NotImplementedException();
        }
        public Task<List<PhotoDetailComment>> PhotoDetailListAsync()
        {
            throw new NotImplementedException();
        }

        private bool PhotoDetailCommentExists(int id)
        {
            return _context.PhotoDetailComment.Any(e => e.Id == id);
        }

    }
}
