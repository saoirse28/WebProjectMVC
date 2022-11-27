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
    public class ReactionRepository : IReactions
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _claimUser;        
        public ReactionRepository(ApplicationDbContext context,
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

        public async Task<List<Reaction>> ReactionListAsync()
        {
            var retVal = await _context.Reaction.ToListAsync();
            return retVal;
        }

        public async Task<Reaction> GetItemAsync(int? id)
        {
            var reaction = await _context.Reaction
                .FirstOrDefaultAsync(m => m.Id == id);
            return reaction;
        }

        public async Task<bool> CreateAsync(Reaction reaction)
        {
            _context.Add(reaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Reaction> EditViewAsync(int? id)
        {
            return await _context.Reaction.FindAsync(id);
        }
        public async Task<Reaction> EditPostAsync(Reaction reaction)
        {
            _context.Update(reaction);
            await _context.SaveChangesAsync();
            return reaction;
        }
        public async Task<Reaction> DeleteItemAsync(int? id)
        {
            var reaction = await _context.Reaction
                .FirstOrDefaultAsync(m => m.Id == id);
            return reaction;
        }
        public async Task<bool> DeleteConfirmedAsync(int id)
        {
            var reaction = await _context.Reaction.FindAsync(id);
            if (reaction != null)
            {
                _context.Reaction.Remove(reaction);
            }
            await _context.SaveChangesAsync();
            return true;
        }
        private bool ReactionExists(int id)
        {
            return _context.Reaction.Any(e => e.Id == id);
        }
    }
}
