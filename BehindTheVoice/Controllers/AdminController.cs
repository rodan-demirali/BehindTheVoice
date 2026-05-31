using Microsoft.AspNetCore.Mvc;

namespace BehindTheVoice.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
