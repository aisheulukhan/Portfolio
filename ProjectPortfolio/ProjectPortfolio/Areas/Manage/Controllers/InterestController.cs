using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPortfolio.DAL;
using ProjectPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPortfolio.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class InterestController : Controller
    {

        private AppDbContext _context { get; }
        public InterestController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Interest> interests = await _context.Interests.ToListAsync();

            return View(interests);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Interest interest)
        {
            if (_context.Interests.FirstOrDefault(c => c.Desc.ToLower().Trim() == interest.Desc.ToLower().Trim()) != null) return RedirectToAction(nameof(Index));
            {
                await _context.Interests.AddAsync(interest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Edit(int id)
        {
            Interest interest = _context.Interests.FirstOrDefault(x => x.Id == id);
            if (interest == null) return NotFound();

            return View(interest);
        }

        [HttpPost]
        public IActionResult Edit(Interest interest)
        {
            Interest existInterest = _context.Interests.FirstOrDefault(x => x.Id == interest.Id);
            if (existInterest == null) return NotFound();

            existInterest.Desc = interest.Desc;
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Remove(int id)
        {
            Interest interest = _context.Interests.Find(id);
            if (interest == null) return NotFound();
            _context.Interests.Remove(interest);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
