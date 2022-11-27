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
    public class ReactionsController : Controller
    {
        private readonly IReactions _reactions;
        public ReactionsController(IReactions reactions)
        {
            _reactions = reactions;
        }

        // GET: Reactions
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _reactions.ReactionListAsync());
        }

        // GET: Reactions/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            var reaction = await _reactions.GetItemAsync(id);
            if (reaction == null)
                return NotFound();
            return View(reaction);
        }

        // GET: Reactions/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Icon,Color,Enabled")] Reaction reaction)
        {
            if (ModelState.IsValid)
            {   
                await _reactions.CreateAsync(reaction);
                return RedirectToAction(nameof(Index));
            }
            return View(reaction);
        }

        // GET: Reactions/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var reaction = await _reactions.EditViewAsync(id);
            if (reaction == null)
            {
                return NotFound();
            }
            return View(reaction);
        }

        // POST: Reactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Icon,Color,Enabled")] Reaction reaction)
        {
            if (id != reaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _reactions.EditPostAsync(reaction);
                return RedirectToAction(nameof(Index));
            }
            return View(reaction);
        }

        // GET: Reactions/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var reaction = await _reactions.DeleteItemAsync(id);
            
            if (reaction == null)
            {
                return NotFound();
            }

            return View(reaction);
        }

        // POST: Reactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await _reactions.DeleteConfirmedAsync(id))
            {
                return Problem("Entity set 'ApplicationDbContext.Reaction'  is null.");
            }
            
            return RedirectToAction(nameof(Index));
        }

        
    }
}
