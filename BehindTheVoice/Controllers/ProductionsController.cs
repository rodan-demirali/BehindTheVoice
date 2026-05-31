using BehindTheVoice.Data;
using BehindTheVoice.Models;
using BehindTheVoice.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BehindTheVoice.Controllers
{
    public class ProductionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductionsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var recentProductions = _context.Productions
                //.Include(vc => vc.Character)
                //.Where(p => p.Type == Models.ProductionType.Movie)
                .OrderByDescending(a => a.UpdatedAt)
                //.Take(4)
                .ToList();
            ViewBag.RecentProductions = recentProductions;


            return View();
        }

        [Route("Productions/{type}/{title}")]
        public IActionResult Details(ProductionType type, string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return NotFound();
            }

            string dbSearchName = title.Replace("_", " ");

            var production = _context.Productions
                    .Include(p => p.Franchise)           
                    .Include(p => p.Genres)              
                    .Include(p => p.VoiceCasts)          
                        .ThenInclude(vc => vc.Character)
                    .Include(p => p.VoiceCasts)
                        .ThenInclude(vc => vc.VoiceActor)
                    .FirstOrDefault(p => p.Title == dbSearchName);

            if (production == null)
            {
                return NotFound();
            }

            return View(production);

        }


        [Route("Productions/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Franchises = _context.Franchises.ToList();
            ViewBag.Genres = _context.Genres.ToList();

            return View();
        }

        [Route("Productions/Create")]
        [HttpPost]
        public IActionResult Create(ProductionCreateViewModel model)
        {
            ModelState.Remove("SelectedGenreIds");

            if (ModelState.IsValid)
            {
                string? sImagePath = null;

                if (model.PosterImage != null)
                {
                    string sFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "productions");
                    if (!Directory.Exists(sFilePath))
                    {
                        Directory.CreateDirectory(sFilePath);
                    }

                    string sUniqueName = Guid.NewGuid().ToString() + "_" + model.PosterImage.FileName;
                    string sFullPath = Path.Combine(sFilePath, sUniqueName);

                    using (var fileStream = new FileStream(sFullPath, FileMode.Create))
                    {
                        model.PosterImage.CopyTo(fileStream);
                    }

                    sImagePath = "~/images/productions/" + sUniqueName;
                }

                var secilenTurler = new List<Genre>();
                if (model.SelectedGenreIds != null && model.SelectedGenreIds.Any())
                {
                    secilenTurler = _context.Genres
                                            .Where(g => model.SelectedGenreIds.Contains(g.Id))
                                            .ToList();
                }

                var yeniProduction = new Production
                {
                    Title = model.Title,
                    Description = model.Description,
                    ReleaseDate = model.ReleaseDate,
                    Studio = model.Studio,
                    Runtime = model.Runtime,                     
                    OriginalLanguage = model.OriginalLanguage,   
                    FranchiseId = model.FranchiseId,             
                    Type = model.Type,
                    PosterUrl = sImagePath,
                    Genres = secilenTurler,                      
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.Productions.Add(yeniProduction);
                _context.SaveChanges();

                return RedirectToAction("Index", "Admin");
            }

            ViewBag.Franchises = _context.Franchises.ToList();
            ViewBag.Genres = _context.Genres.ToList();

            return View(model);
        }


    }
}
