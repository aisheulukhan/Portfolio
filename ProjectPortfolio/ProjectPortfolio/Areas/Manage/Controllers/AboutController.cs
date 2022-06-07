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
    public class AboutController : Controller
    {
        private AppDbContext _context { get; }
        public AboutController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<About> abouts = await _context.Abouts.ToListAsync();

            return View(abouts);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(About about)
        {
            if (_context.Abouts.FirstOrDefault(c => c.Name.ToLower().Trim() == about.Name.ToLower().Trim()) != null) return RedirectToAction(nameof(Index));
            {
                await _context.Abouts.AddAsync(about);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Edit(int id)
        {
            About about = _context.Abouts.FirstOrDefault(x => x.Id == id);
            if (about == null) return NotFound();

            return View(about);
        }

        [HttpPost]
        public IActionResult Edit(About about)
        {
            About existAbout = _context.Abouts.FirstOrDefault(x => x.Id == about.Id);
            if (existAbout == null) return NotFound();

            existAbout.Name = about.Name;
            existAbout.Surname = about.Surname;
            existAbout.Address = about.Address;
            existAbout.Number = about.Number;
            existAbout.Email = about.Email;
            existAbout.Desc = about.Desc;
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Remove(int id)
        {
            About about = _context.Abouts.Find(id);
            if (about == null) return NotFound();
            _context.Abouts.Remove(about);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
