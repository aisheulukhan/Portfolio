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
    public class ToolsController : Controller
    {
        private AppDbContext _context { get; }
        public ToolsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Tools> tools = await _context.Tools.ToListAsync();

            return View(tools);
        }

        public IActionResult Edit(int id)
        {
            Tools tools = _context.Tools.FirstOrDefault(x => x.Id == id);
            if (tools == null) return NotFound();

            return View(tools);
        }

        [HttpPost]
        public IActionResult Edit(Tools tools)
        {
            Tools existTools = _context.Tools.FirstOrDefault(x => x.Id == tools.Id);
            if (existTools == null) return NotFound();

            existTools.Image = tools.Image;
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Remove(int id)
        {
            Tools tools = _context.Tools.Find(id);
            if (tools == null) return NotFound();
            _context.Tools.Remove(tools);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
