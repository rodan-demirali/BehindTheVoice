using Microsoft.AspNetCore.Mvc;
using BehindTheVoice.Data;
using BehindTheVoice.Models;

namespace BehindTheVoice.Controllers
{
    public class GenresController : Controller
    {
        private readonly AppDbContext _context;

        public GenresController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.Genres.Add(genre);
                _context.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            return View(genre);
        }
    }
}