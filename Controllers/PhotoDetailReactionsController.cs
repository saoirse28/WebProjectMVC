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
    public class PhotoDetailReactionsController : Controller
    {
        private IPhotoDetailReactions _photoDetailReactions;

        public PhotoDetailReactionsController(IPhotoDetailReactions photoDetailReactions)
        {
            _photoDetailReactions = photoDetailReactions;
        }
        [Authorize]
        public async Task<IActionResult> TagReaction([FromQuery] PhotoDetailReactions photoDetailReactions)
        {
            var b = await _photoDetailReactions.TagPhotoDetailReaction(photoDetailReactions);
            return RedirectToAction("Details", "PhotoDetails", new { id = photoDetailReactions.PhotoDetailId });
        }
    }
}
