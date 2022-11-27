using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebProjectMVC.Data;
using WebProjectMVC.Models;
using WebProjectMVC.ViewModels;

namespace WebProjectMVC.Interface.Services
{
    public class PhotoReactionRepository : IPhotoReactions
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _claimUser;
        public PhotoReactionRepository(ApplicationDbContext context,
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

        public  List<PhotoReactionCount> GetPhotoReaction(PhotoReactions photoReactions)
        {

            photoReactions.PhotoId = 23;
            photoReactions.ReactionId = 1;

            var reactList = from line in _context.Reaction
                        join react in _context.PhotoReactions on line.Id equals react.ReactionId
                        join photo in _context.Photos on react.PhotoId equals photo.Id
                        group line by react.ReactionId into lineGrouping
                        select lineGrouping;

            List<PhotoReactionCount> count = new List<PhotoReactionCount>();
            return count;
        }

        public async Task<bool> TagPhotoReaction(PhotoReactions photoReactions)
        {
            var user = await _userManager.GetUserAsync(_claimUser);

            var find = await (from r in _context.PhotoReactions
                              where r.PhotoId.Equals(photoReactions.PhotoId)
                              && r.ApplicationUserId.Equals(user.Id)
                              select r).AsNoTracking().ToListAsync();

            var photoReaction = new PhotoReactions
            {
                PhotoId = photoReactions.PhotoId,
                ReactionId = photoReactions.ReactionId,
                ApplicationUserId = user.Id
            };


            if (find.Any())
            {
                _context.PhotoReactions.Remove(photoReaction);
                await _context.SaveChangesAsync();

                if (find[0].ReactionId != photoReactions.ReactionId)
                {
                    _context.Add(photoReaction);
                    await _context.SaveChangesAsync();
                }
                find.Clear();
            }
            else
            {
                _context.Add(photoReaction);
                await _context.SaveChangesAsync();
            }
            return true;
        }

    }
}
