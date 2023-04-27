using Blog.DataAccess.Repository.IRepository;
using Blog.Models;
using Blog.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Blog.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitofWork _unitofwork;
        private readonly SignInManager<User> _signInManager;
        public UserController(IUnitofWork unitOfWork, SignInManager<User> signInManager)
        {
            _signInManager=signInManager;
            _unitofwork = unitOfWork;
        }
        // GET: UserController
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = _unitofwork.User.GetFirstorDefault(u => u.Email == model.Email);
            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                {
                    await _signInManager.SignInAsync(user,false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("","Invalid Username or Password");
                return View();
            }
        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        // GET: UserController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_unitofwork.User.GetFirstorDefault(u=>u.NormalizedEmail==model.Email.ToUpper())==null)
                    {
                        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
                        var user = new User
                        {
                            Email = model.Email,
                            NormalizedEmail = model.Email.ToUpper(),
                            UserName = model.Name,
                            NormalizedUserName = model.Name.ToUpper(),
                            PasswordHash = hashedPassword
                        };
                        _unitofwork.User.Add(user);
                        _unitofwork.Save();
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "This email already registered");
                    }
                    
                }
                else
                {
                    var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                    foreach (var errorMessage in errorMessages)
                    {
                        ModelState.AddModelError("", errorMessage);
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

    }
}
