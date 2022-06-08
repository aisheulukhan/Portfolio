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
    public class ExperienceController : Controller
    {
        
        private AppDbContext _context { get; }
        public ExperienceController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Experience> experiences = await _context.Experiences.ToListAsync();

            return View(experiences);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Experience experience)
        {
            if (_context.Experiences.FirstOrDefault(c => c.Title1.ToLower().Trim() == experience.Title1.ToLower().Trim()) != null) return RedirectToAction(nameof(Index));
            {
                await _context.Experiences.AddAsync(experience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Edit(int id)
        {
            Experience experience = _context.Experiences.FirstOrDefault(x => x.Id == id);
            if (experience == null) return NotFound();

            return View(experience);
        }

        [HttpPost]
        public IActionResult Edit(Experience experience)
        {
            Experience existExperience = _context.Experiences.FirstOrDefault(x => x.Id == experience.Id);
            if (existExperience == null) return NotFound();

            existExperience.Title1 = experience.Title1;
            existExperience.Title2 = experience.Title2;
            existExperience.Desc = experience.Desc;
            existExperience.Date = experience.Date;
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Remove(int id)
        {
            Experience experience = _context.Experiences.Find(id);
            if (experience == null) return NotFound();
            _context.Experiences.Remove(experience);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

