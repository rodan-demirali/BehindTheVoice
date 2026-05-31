using BehindTheVoice.Data;
using BehindTheVoice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BehindTheVoice.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
           _context = context;
        }

        public IActionResult Index()
        {
            var recentActors = _context.VoiceActors
                .OrderByDescending(a => a.UpdatedAt)
                .Take(4)
                .ToList();
            ViewBag.RecentActors = recentActors;

            var recentMovies = _context.Productions
                .Where(p => p.Type == Models.ProductionType.Movie)
                .OrderByDescending(a => a.UpdatedAt)
                .Take(4)
                .ToList();
            ViewBag.RecentMovies = recentMovies;


            var recentCharacters = _context.VoiceCasts
                .Include(vc => vc.Character)
                .Include(vc => vc.Production)
                .OrderByDescending(a => a.UpdatedAt)
                .Take(4)
                .ToList();
            ViewBag.RecentCharacters = recentCharacters;


            return View();
        }

        public IActionResult VoiceActors()
        {


            return RedirectToAction("Index" ,"VoiceActors");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
