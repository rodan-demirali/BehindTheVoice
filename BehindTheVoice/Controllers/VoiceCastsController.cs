using BehindTheVoice.Data;
using BehindTheVoice.Models;
using BehindTheVoice.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting; // IWebHostEnvironment için

namespace BehindTheVoice.Controllers
{
    public class VoiceCastsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VoiceCastsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // 1. FORMU GÖRÜNTÜLEME METODU
        [HttpGet]
        public IActionResult Create()
        {
            // Dropdown (Açılır liste) içini doldurmak için verileri çekip View'a gönderiyoruz
            ViewBag.VoiceActors = _context.VoiceActors.OrderBy(v => v.FullName).ToList();
            ViewBag.Characters = _context.Characters.OrderBy(c => c.Name).ToList();
            ViewBag.Productions = _context.Productions.OrderBy(p => p.Title).ToList();

            return View();
        }

        // 2. FORMU KAYDETME METODU
        [HttpPost]
        public IActionResult Create(VoiceCastCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string? sImagePath = null;

                // Eğer o yapıma özel bir karakter görünümü yüklendiyse:
                if (model.CharacterAppearanceImage != null)
                {
                    string sFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "appearances");
                    if (!Directory.Exists(sFilePath))
                    {
                        Directory.CreateDirectory(sFilePath);
                    }

                    string sUniqueName = Guid.NewGuid().ToString() + "_" + model.CharacterAppearanceImage.FileName;
                    string sFullPath = Path.Combine(sFilePath, sUniqueName);

                    using (var fileStream = new FileStream(sFullPath, FileMode.Create))
                    {
                        model.CharacterAppearanceImage.CopyTo(fileStream);
                    }

                    sImagePath = "~/images/appearances/" + sUniqueName;
                }

                // Eşleşme (Kavşak) modelimizi oluşturuyoruz
                var yeniKadro = new VoiceCast
                {
                    VoiceActorId = model.VoiceActorId,
                    CharacterId = model.CharacterId,
                    ProductionId = model.ProductionId,
                    LanguageCode = model.LanguageCode,
                    CharacterAppearanceUrl = sImagePath, // Özel resim (Varsa)
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.VoiceCasts.Add(yeniKadro);
                _context.SaveChanges();

                // İşlem bitince Admin Paneline dön
                return RedirectToAction("Index", "Admin");
            }

            // Hata durumunda dropdownların patlamaması için listeleri tekrar dolduruyoruz
            ViewBag.VoiceActors = _context.VoiceActors.OrderBy(v => v.FullName).ToList();
            ViewBag.Characters = _context.Characters.OrderBy(c => c.Name).ToList();
            ViewBag.Productions = _context.Productions.OrderBy(p => p.Title).ToList();

            return View(model);
        }
    }
}