﻿using Blog.DataAccess.Repository.IRepository;
using Blog.Models;
using Blog.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            var user = _unitofwork.User.GetFirstorDefault(u => u.EmailAddress == model.Email);
            if (user!=null)
            {
                if ()
                {

                }
                    await _signInManager.SignInAsync(user, isPersistent:false);


                    ModelState.AddModelError("", "Invalid Username or Password");
                    return View();
            }
            else
            {
                ModelState.AddModelError("","Invalid Username or Password");
                return View();
            }
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
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
                    var user = new User
                    {
                        EmailAddress = model.Email,
                        UserName = model.Name,
                        Password = hashedPassword,
                        Role = "User"
                    };
                    _unitofwork.User.Add(user);
                    _unitofwork.Save();
                    return RedirectToAction("Login");
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