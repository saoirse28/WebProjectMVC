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
    public class PhotoDetailsController : Controller
    {
        private readonly IPhotoDetails _photoDetails;
        public PhotoDetailsController(IPhotoDetails photoDetails)
        {
            _photoDetails = photoDetails;
        }
        // GET: PhotoDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var photoViewDetail = await _photoDetails.GetItemAsync(id);

            if (photoViewDetail.PhotoDetail == null)
            {
                return NotFound();
            }

            return View(photoViewDetail);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var photoViewDetail = await _photoDetails.GetItemAsync(id);

            if (photoViewDetail.PhotoDetail == null)
            {
                return NotFound();
            }

            return View(photoViewDetail);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var photoId = await _photoDetails.DeleteConfirmedAsync(id);

            if (photoId==0)
            {
                return RedirectToAction("Index", "Photos");
            }
            return RedirectToAction("Details","Photos",new {id = photoId});
        }
    }
}
