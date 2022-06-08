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
    public class WorkController : Controller
    {
        private AppDbContext _context { get; }
        public WorkController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Work> works = await _context.Works.ToListAsync();

            return View(works);
        }

        public IActionResult Edit(int id)
        {
            Work work = _context.Works.FirstOrDefault(x => x.Id == id);
            if (work == null) return NotFound();

            return View(work);
        }

        [HttpPost]
        public IActionResult Edit(Work work)
        {
            Work existWork = _context.Works.FirstOrDefault(x => x.Id == work.Id);
            if (existWork == null) return NotFound();

            existWork.Desc = work.Desc;
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Remove(int id)
        {
            Work work = _context.Works.Find(id);
            if (work == null) return NotFound();
            _context.Works.Remove(work);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
