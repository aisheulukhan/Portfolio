using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPortfolio.DAL;
using ProjectPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context { get; }
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Abouts = await _context.Abouts.ToListAsync(),
                Awards = await _context.Awards.ToListAsync(),
                Educations = await _context.Educations.ToListAsync(),
                Experiences = await _context.Experiences.ToListAsync(),
                Interests = await _context.Interests.ToListAsync(),
                Tools = await _context.Tools.ToListAsync(),
                Workflows = await _context.Workflows.ToListAsync(),
                Works = await _context.Works.ToListAsync()
            };
            return View(homeVM);


        }
    }

}