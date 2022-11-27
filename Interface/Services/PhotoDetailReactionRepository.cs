using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebProjectMVC.Data;
using WebProjectMVC.Models;
using WebProjectMVC.ViewModels;

namespace WebProjectMVC.Interface.Services
{
    public class PhotoDetailReactionRepository : IPhotoDetailReactions
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _claimUser;
        public PhotoDetailReactionRepository(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _claimUser = httpContextAccessor.HttpContext.User;
        }

        public async Task<bool> TagPhotoDetailReaction(PhotoDetailReactions photoDetailReactions)
        {
            var user = await _userManager.GetUserAsync(_claimUser);

            var find = await (from r in _context.PhotoDetailReactions
                       where r.PhotoDetailId.Equals(photoDetailReactions.PhotoDetailId) 
                       && r.ApplicationUserId.Equals(user.Id)
                       select r).AsNoTracking().ToListAsync();

            var photoDetailReaction = new PhotoDetailReactions
            {
                PhotoDetailId = photoDetailReactions.PhotoDetailId,
                ReactionId = photoDetailReactions.ReactionId,
                ApplicationUserId = user.Id
            };


            if (find.Any())
            {
                _context.PhotoDetailReactions.Remove(photoDetailReaction);
                await _context.SaveChangesAsync();

                if (find[0].ReactionId != photoDetailReaction.ReactionId)
                {
                    _context.Add(photoDetailReaction);
                    await _context.SaveChangesAsync();
                }
                find.Clear();
            }
            else
            {
                _context.Add(photoDetailReaction);
                await _context.SaveChangesAsync();
            }

            return true;

        }

    }
}
