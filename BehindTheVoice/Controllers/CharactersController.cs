using BehindTheVoice.Data;
using BehindTheVoice.Models;
using BehindTheVoice.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BehindTheVoice.Controllers
{
    public class CharactersController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CharactersController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var recentCharacters = _context.VoiceCasts
                .Include(vc => vc.Character)
                .Include(vc => vc.Production)
                .OrderByDescending(a => a.UpdatedAt)
                //.Take(4)
                .ToList();
            ViewBag.RecentCharacters = recentCharacters;

            return View();
        }

        [Route("Characters/{productionTitle}/{name}")]
        public IActionResult Details(string productionTitle, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return NotFound();
            }

            string dbSearchName = name.Replace("_", " ");

            var character = _context.Characters
                .Include(v => v.VoiceCasts)
                .ThenInclude(vc => vc.VoiceActor)
                .Include(v => v.VoiceCasts)
                .ThenInclude(vc => vc.Production)
                .FirstOrDefault(v => v.Name== dbSearchName);

            if (character == null)
            {
                return NotFound();
            }

            return View(character);

        }

        [Route("Characters/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Characters/Create")]
        [HttpPost]
        public IActionResult Create(CharacterCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string? sImagePath = null;

                if (model.CharacterImage!= null)
                {
                    string sFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "characters");
                    if(!Directory.Exists(sFilePath))
                    {
                        Directory.CreateDirectory(sFilePath);
                    }

                    string sUniqueName = Guid.NewGuid().ToString() + "_" + model.CharacterImage.FileName;
                    string sFullPath = Path.Combine(sFilePath, sUniqueName);

                    using (var fileStream = new FileStream(sFullPath, FileMode.Create))
                    {
                        model.CharacterImage.CopyTo(fileStream);
                    }

                    sImagePath = "~/images/characters/" + sUniqueName;
                }

                var yeniKarakter = new Character
                {
                    
                    Name = model.Name,
                    ImageUrl = sImagePath,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,

                    //Name = model.
                    //ImageUrl = sImagePath,
                    //CreatedAt = DateTime.Now,
                    //UpdatedAt = DateTime.Now,
                    //Description = model.Description,
                    //BirthPlace = model.BirthPlace,
                    //DateOfBirth = model.DateOfBirth,

                };

                _context.Characters.Add(yeniKarakter);
                _context.SaveChanges();

                return RedirectToAction("Index", "Admin");

            }

            return View(model);
        }


    }
}
