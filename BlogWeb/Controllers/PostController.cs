using Blog.DataAccess.Repository.IRepository;
using Blog.Models;
using Blog.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace BlogWeb.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitofWork _unitofwork;
        string currentUserId = null;
        public PostController(IWebHostEnvironment webHostEnvironment, IUnitofWork unitOfWork)
        {
            _webHostEnvironment = webHostEnvironment;
            _unitofwork = unitOfWork;
        }
        public IActionResult CreatePost()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(CreatePostViewModel post)
        {
            if (ModelState.IsValid && post.Img!=null)
            {
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string imageName = Guid.NewGuid().ToString() + "_" + post.Img.FileName;
                string imagepath = Path.Combine(uploadPath, imageName);
                using (var fileStream = new FileStream(imagepath, FileMode.Create))
                {
                    await post.Img.CopyToAsync(fileStream);
                }

                var bPost = new BlogPost
                {
                    Title = post.Title,
                    Description = post.Description,
                    ImgUrl = "images/" + imageName,
                    CreationTime = DateTime.Now,
                    UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
                };
                _unitofwork.Post.Add(bPost);
                _unitofwork.Save();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(post);
            }
            
        }

        public IActionResult PersonalPosts()
        {
            var currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<BlogPost> posts = _unitofwork.Post.GetAll(u => u.UserId ==currentUserId ,"user");
            return View(posts);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int Id)
        {
            var post = _unitofwork.Post.GetFirstorDefault(u => u.Id == Id);
            var comments = _unitofwork.Comment.GetAll(u => u.postId == Id);
            _unitofwork.Comment.RemoveRange(comments);
            _unitofwork.Post.Remove(post);
            _unitofwork.Save();
            return RedirectToAction("PersonalPosts");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(string commentContent, int postIdNumber)
        {
            var newComment = new Comment
            {
                userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value,
                postId = postIdNumber,
                content = commentContent
            };
            _unitofwork.Comment.Add(newComment);
            _unitofwork.Save();
            return RedirectToAction("Details", "Home", new {@id=postIdNumber});
        }
    }
}
