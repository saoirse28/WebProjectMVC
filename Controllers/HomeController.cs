using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebProjectMVC.Data;

namespace WebProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IHttpContextAccessor _httpContextAccessor;
        private ClaimsPrincipal _claimUser;
        public IActionResult Index()
        {
            return View();
        }
    }
}
