using BehindTheVoice.Data;
using BehindTheVoice.Models;
using Microsoft.AspNetCore.Mvc;

namespace BehindTheVoice.Controllers
{
    public class FranchisesController : Controller
    {
        private readonly AppDbContext _context;

        public FranchisesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Franchise franchise)
        {
            if (ModelState.IsValid)
            {
                _context.Franchises.Add(franchise);
                _context.SaveChanges();

                return RedirectToAction("Index", "Admin");
            }
            return View(franchise);
        }
    }
}
