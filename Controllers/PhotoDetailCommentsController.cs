using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProjectMVC.Data;
using WebProjectMVC.Interface;
using WebProjectMVC.Models;

namespace WebProjectMVC.Controllers
{
    public class PhotoDetailCommentsController : Controller
    {
        private readonly IPhotoDetailComments _photoDetailComments;
        public PhotoDetailCommentsController(IPhotoDetailComments photoDetailComments)
        {
            _photoDetailComments = photoDetailComments;
        }

        // POST: PhotoDetailComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Detail,ApplicationUserId,PhotoDetailId")] PhotoDetailComment photoDetailComment)
        {
            if (await _photoDetailComments.CreateAsync(photoDetailComment))
            {
                return RedirectToAction("Details","PhotoDetails", new { id = photoDetailComment.PhotoDetailId });
            }
            return View(photoDetailComment);
        }

        // POST: PhotoDetailComments/Delete/5
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var photoDetailId = await _photoDetailComments.DeleteConfirmedAsync(id);
            if (photoDetailId ==0)
            {
                return RedirectToAction("Index", "Photos");
            }
            return RedirectToAction("Details", "PhotoDetails", new {Id = photoDetailId });
        }

        
    }
}
