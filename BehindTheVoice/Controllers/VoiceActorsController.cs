using BehindTheVoice.Data;
using BehindTheVoice.Models;
using BehindTheVoice.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BehindTheVoice.Controllers
{
    public class VoiceActorsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        public VoiceActorsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var recentActors = _context.VoiceActors
                .OrderByDescending(a => a.UpdatedAt)
                //.Take(4)
                .ToList();
            ViewBag.RecentActors = recentActors;

            //var recentMovies = _context.Productions
            //    .Where(p => p.Type == Models.ProductionType.Movie)
            //    .OrderByDescending(a => a.UpdatedAt)
            //    .Take(4)
            //    .ToList();
            //ViewBag.RecentMovies = recentMovies;


            //var recentCharacters = _context.VoiceCasts
            //    .Include(vc => vc.Character)
            //    .Include(vc => vc.Production)
            //    .OrderByDescending(a => a.UpdatedAt)
            //    .Take(4)
            //    .ToList();
            //ViewBag.RecentCharacters = recentCharacters;


            return View();
        }

        [Route("VoiceActors/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("VoiceActors/Create")]
        [HttpPost]
        public IActionResult Create(VoiceActorCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                string? sImagePath = null;

                if(model.ProfileImage != null)
                {
                    string sFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string sUniqueName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;

                    string sFullPath = Path.Combine(sFilePath, sUniqueName);

                    using(var fileStream = new FileStream(sFullPath, FileMode.Create))
                    {
                        model.ProfileImage.CopyTo(fileStream);
                    }

                    sImagePath = "~/images/" + sUniqueName;
                }

                var yeniAktor = new VoiceActor
                {
                    FullName = model.FullName,
                    ImageUrl = sImagePath,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Description = model.Description,
                    BirthPlace = model.BirthPlace,
                    DateOfBirth = model.DateOfBirth,

                };

                _context.VoiceActors.Add(yeniAktor);
                _context.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(model);
        }


        [Route("VoiceActors/{name}")]
        public IActionResult Details(string name)
        {
            if(string.IsNullOrEmpty(name))
            {   
                return NotFound();
            }

            string dbSearchName = name.Replace("_", " ");

            var actor = _context.VoiceActors
                .Include(v => v.VoiceCasts)
                .ThenInclude(vc => vc.Character)
                .Include(v => v.VoiceCasts)
                .ThenInclude(vc => vc.Production)
                .FirstOrDefault(v => v.FullName == dbSearchName);

            if(actor == null)
            {
                return NotFound();
            }

            return View(actor);
            
        }
    }
}
