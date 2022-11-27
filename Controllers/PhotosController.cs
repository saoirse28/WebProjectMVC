using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProjectMVC.Data;
using WebProjectMVC.Models;
using WebProjectMVC.Interface;
using WebProjectMVC.ViewModels;
using WebProjectMVC.Interface.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebProjectMVC.Controllers
{

    public class PhotosController : Controller
    {
        private readonly IPhotos _photos;
        public PhotosController(IPhotos photo)
        {
            _photos = photo;
        }

        // GET: Photos
        public async Task<IActionResult> Index()
        {
            return View(await _photos.PhotoListAsync());
        }

        // GET: Photos/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            var photo = await _photos.GetItemAsync(id);

            if (id == null || photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        // GET: Photos/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Photos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return View(photo);
            }

           if (!await _photos.CreateAsync(photo))
            {
                ModelState.AddModelError("File", "Error uploading pictures.");
            }
            return RedirectToAction("Index");
        }


        // GET: Photos/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var photo = await _photos.EditViewAsync(id);
            if (photo == null)
            {
                return NotFound();
            }
            return View(photo);
        }

        // POST: Photos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [FromForm] Photo photo)
        {
            var retVal = await _photos.EditPostAsync(photo);

            if (id != retVal.Id)
                return NotFound();
            if (!ModelState.IsValid)
                return View(photo);
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Photos/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var photo = await _photos.DeleteItemAsync(id);
            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (!await _photos.DeleteConfirmedAsync(id))
            {
                return Problem("Error deleting record.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
