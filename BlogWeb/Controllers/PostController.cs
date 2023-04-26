using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        public IActionResult CreatePost()
        {
            
            return View();
        }
    }
}
