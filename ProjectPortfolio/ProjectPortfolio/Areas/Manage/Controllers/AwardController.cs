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
    public class AwardController : Controller
    {

        private AppDbContext _context { get; }
        public AwardController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Award> awards = await _context.Awards.ToListAsync();

            return View(awards);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Award award)
        {
            if (_context.Awards.FirstOrDefault(c => c.Desc.ToLower().Trim() == award.Desc.ToLower().Trim()) != null) return RedirectToAction(nameof(Index));
            {
                await _context.Awards.AddAsync(award);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Edit(int id)
        {
            Award award = _context.Awards.FirstOrDefault(x => x.Id == id);
            if (award == null) return NotFound();

            return View(award);
        }

        [HttpPost]
        public IActionResult Edit(Award award)
        {
            Award existAward = _context.Awards.FirstOrDefault(x => x.Id == award.Id);
            if (existAward == null) return NotFound();

            existAward.Desc = award.Desc;
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Remove(int id)
        {
            Education education = _context.Educations.Find(id);
            if (education == null) return NotFound();
            _context.Educations.Remove(education);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
