using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using WebProjectMVC.Data;
using WebProjectMVC.Interface;
using WebProjectMVC.Models;

namespace WebProjectMVC.Controllers
{
    public class PhotoReactionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IPhotoReactions _photoReactions;

        public PhotoReactionsController(IPhotoReactions photoReactions)
        {
            _photoReactions = photoReactions;
        }

        [Authorize]
        public async Task<IActionResult> TagReaction([FromQuery] PhotoReactions photoReactions)
        {
            var b = await _photoReactions.TagPhotoReaction(photoReactions);
            return RedirectToAction("Details", "Photos", new { id = photoReactions.PhotoId });
        }
    }
}
