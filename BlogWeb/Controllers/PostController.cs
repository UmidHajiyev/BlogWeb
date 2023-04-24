using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
