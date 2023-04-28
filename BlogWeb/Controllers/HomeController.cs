using Blog.DataAccess.Repository.IRepository;
using Blog.Models;
using Blog.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWork _unitofwork;
        public HomeController(ILogger<HomeController> logger, IUnitofWork unitofWork)
        {
            _logger = logger;
            _unitofwork = unitofWork;
        }

        public IActionResult Index()
        {
            IEnumerable<BlogPost> posts = _unitofwork.Post.GetAll(includeproperties:"user");
            return View(posts);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var postDetail = new PostDetailViewModel
            {
                post = _unitofwork.Post.GetFirstorDefault(u => u.Id == id, includeproperties: "user"),
                comments = _unitofwork.Comment.GetAll(u => u.postId == id, includeproperties: "user")
            };
            return View(postDetail);
        }
        [HttpPost]
        public IActionResult CreatePost(BlogPost post)
        {
            return View();
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